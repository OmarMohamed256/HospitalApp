import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorServiceService } from 'src/app/core/services/doctor-service.service';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { MedicineService } from 'src/app/core/services/medicine.service';
import { ServiceService } from 'src/app/core/services/service.service';
import { CreateInvoice } from 'src/app/models/InvoiceModels/createInvoice';
import { CreateInvoiceDoctorService } from 'src/app/models/InvoiceModels/createInvoiceDoctorService';
import { MedicineParams } from 'src/app/models/Params/medicineParams';
import { Appointment } from 'src/app/models/appointment';
import { DoctorService } from 'src/app/models/doctorService';
import { Medicine } from 'src/app/models/medicine';
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
  medicineList: Medicine[] = [];
  selectedMedicines: Medicine[] = [];
  medicineParams: MedicineParams = {
    pageNumber: 1,
    pageSize: 5,
  };

  constructor(private fb: FormBuilder, private route: ActivatedRoute,
    private doctorServiceService: DoctorServiceService, private serviceService: ServiceService,
    private invoiceService: InvoiceService, private router: Router, private medicineService: MedicineService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.appointment = data['appointment'];
      this.getDoctorServicesByDoctorId();
      this.initializeForm();
    })
  }

  compareFn(item1: any, item2: any): boolean {
    return item1 && item2 ? item1.id === item2.id : item1 === item2;
  }

  searchMedicines(event: any) {
    if (event.term.trim().length > 1) {
      this.medicineParams.searchTerm = event.term;
      this.medicineService.getMedicines(this.medicineParams).subscribe(response => {
        this.medicineList = response.result;
      });
    }
  }

  onMedicineSelect(medicines: Medicine[]) {
    this.updateSelectedMedicineItems(medicines);
  }

  updateSelectedMedicineItems(medicines: any) {
    const selectedMedicineItemsFormArray = this.createInvoiceForm.get('invoiceMedicines') as FormArray;
    selectedMedicineItemsFormArray.clear();
    medicines.forEach((medicine: any) => {
      selectedMedicineItemsFormArray.push(this.fb.group({
        medicineId: [medicine.id],
      }));
    });
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
      invoiceMedicines: this.fb.array([])
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
      customItems: this.createInvoiceForm.get('customItems')?.value,
      invoiceMedicines: this.createInvoiceForm.get('invoiceMedicines')?.value
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
