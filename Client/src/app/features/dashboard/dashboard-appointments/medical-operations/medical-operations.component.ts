import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorServiceService } from 'src/app/core/services/doctor-service.service';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { ServiceService } from 'src/app/core/services/service.service';
import { Appointment } from 'src/app/models/appointment';
import { CreateInvoice } from 'src/app/models/createInvoice';
import { CreateInvoiceDoctorService } from 'src/app/models/createInvoiceDoctorService';
import { DoctorService } from 'src/app/models/doctorService';
import { Service } from 'src/app/models/service';

@Component({
  selector: 'app-medical-operations',
  templateUrl: './medical-operations.component.html',
  styleUrls: ['./medical-operations.component.scss']
})
export class MedicalOperationsComponent {
  appointment?: Appointment;
  doctor_services: DoctorService[] | null = [];
  services: Service[] | null = [];
  selectedServices: Service[] | null = [];
  validationErrors: string[] = [];
  createInvoiceForm!: FormGroup;

  constructor(private fb: FormBuilder, private route: ActivatedRoute,
    private doctorServiceService: DoctorServiceService, private serviceService: ServiceService,
    private invoiceService: InvoiceService, private router: Router) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.appointment = data['appointment'];
      this.getDoctorServicesByDoctorId();
      this.initializeForm();
    })
  }

  getDoctorServicesByDoctorId() {
    this.doctorServiceService.getDoctorServiceByDocorId(this.appointment!.doctorId.toString()).subscribe(response => {
      this.doctor_services = response;
      this.services = this.doctor_services.map(item => item.service);
    });
  }

  initializeForm() {
    this.createInvoiceForm = this.fb.group({
      invoiceSelectedServices: this.fb.array([]),
      customItems: this.fb.array([]),
    });
  }

  pushNewCustomItem() {
    const customItemsArray = this.createInvoiceForm.get('customItems') as FormArray;
    customItemsArray.push(this.fb.group({
      name: ['', Validators.required],
      price: [1, [Validators.required, Validators.min(1)]],
      units: [1, [Validators.required, Validators.min(1)]],
    }));
  }

  removeCustomItem(index: number) {
    const selectedCustomItemsFormArray = this.createInvoiceForm.get('customItems') as FormArray;
    selectedCustomItemsFormArray.removeAt(index);
  }

  onServiceSelect(services: Service[]) {
    this.updateSelectedService(services);
  }

  invoiceRemoveService(removedService: any) {
    const selectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;
    const index = selectedServicesFormArray.controls.findIndex(control => {
      return control.get('serviceId')?.value === removedService.value.id;
    });
    if (index !== -1) {
      selectedServicesFormArray.removeAt(index);
    }
  }
  updateSelectedService(items: Service[]) {
    const selectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;

    items.forEach((item: Service) => {
      const serviceId = item.id;

      // Check if the serviceId already exists in selectedServicesFormArray
      const serviceAlreadyExists = selectedServicesFormArray.controls.some(control => {
        return control.get('serviceId')?.value === serviceId;
      });

      if (!serviceAlreadyExists) {
        this.getServiceDisposablePrice(serviceId, 1).subscribe(serviceDisposablePrice => {
          selectedServicesFormArray.push(this.fb.group({
            serviceName: [{ value: item.name, disabled: true }, Validators.required],
            servicePrice: [{ value: item.totalPrice, disabled: true }, Validators.required],
            quantity: [1, [Validators.required, Validators.min(1)]],
            serviceId: [item.id, Validators.required],
            servicetotalPrice: [{ value: item.totalPrice + serviceDisposablePrice, disabled: true }, Validators.required],
            serviceDisposablePrice: [serviceDisposablePrice],
          }));
        });
      }
    });
  }
  getServiceDisposablePrice(id: number, serviceQuantity: number) {
    return this.serviceService.getServiceDispsablesPrice(id, serviceQuantity);
  }
  quantityChanged(index: number) {
    const selectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;
    const control = selectedServicesFormArray.at(index);

    var quantity = control.getRawValue().quantity;
    if (quantity <= 0) return;
    var serviceId = control.getRawValue().serviceId;
    var servicePrice = control.getRawValue().servicePrice;

    this.getServiceDisposablePrice(serviceId, quantity).subscribe({
      next: (serviceDisposablePrice) => {
        control.get('disposablesPrice')?.setValue(serviceDisposablePrice);
        var totalPrice = (servicePrice * quantity) + serviceDisposablePrice;
        control.get('servicetotalPrice')?.setValue(totalPrice);
      },
      error: (err) => {
        control.get('quantity')?.setValue(1);
      }
    });
  }

  mapInvoiceFormToCreateInvoice() {
    const createInvoice: CreateInvoice = {
      id: 0,
      appointmentId: this.appointment?.id!,
      paymentMethod: "cash",
      discountPercentage: 0,
      totalPaid: 0,
      invoiceDoctorServices: this.mapCreateDoctorService(),
      customItems: this.createInvoiceForm.get('customItems')?.value
    };
    return createInvoice;
  }

  createInvoice() {
    var invoice = this.mapInvoiceFormToCreateInvoice();
    this.invoiceService.createInvoice(invoice).subscribe(response => {
      console.log(response);
    })
  }

  mapCreateDoctorService() {
    const doctorServices: CreateInvoiceDoctorService[] = [];

    const invoiceSelectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;
    
    invoiceSelectedServicesFormArray.controls.forEach(control => {
      const doctorService: CreateInvoiceDoctorService = {
        doctorServiceId: this.doctor_services?.find(s => s.serviceId == control.get('serviceId')?.value)?.id!,
        serviceQuantity: control.get('quantity')?.value
      };
      doctorServices.push(doctorService);
    });
    return doctorServices;
  }
}
