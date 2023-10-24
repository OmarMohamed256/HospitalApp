import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { PaymentList } from 'src/app/constants/paymentMethods';
import { DoctorServiceService } from 'src/app/core/services/doctor-service.service';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { MedicineService } from 'src/app/core/services/medicine.service';
import { ServiceService } from 'src/app/core/services/service.service';
import { Appointment } from 'src/app/models/appointment';
import { DoctorService } from 'src/app/models/doctorService';
import { CreateInvoice } from 'src/app/models/InvoiceModels/createInvoice';
import { CreateInvoiceDoctorService } from 'src/app/models/InvoiceModels/createInvoiceDoctorService';
import { Invoice } from 'src/app/models/InvoiceModels/invoice';
import { Medicine } from 'src/app/models/medicine';
import { MedicineParams } from 'src/app/models/Params/medicineParams';
import { Service } from 'src/app/models/service';

@Component({
  selector: 'app-finalize-appointment',
  templateUrl: './finalize-appointment.component.html',
  styleUrls: ['./finalize-appointment.component.scss']
})
export class FinalizeAppointmentComponent implements OnInit {
  appointment?: Appointment;
  invoice?: Invoice;
  doctor_services: DoctorService[] | null = [];
  services: Service[] | null = [];
  selectedServices: Service[] | null = [];
  validationErrors: string[] = [];
  paymentMethodList = PaymentList;
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
      this.invoice = data['invoice'];
      this.appointment = this.invoice?.appointment;
      this.getDoctorServicesByDoctorId();
      this.initializeForm();
      this.intializeMedicineList();
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
  intializeMedicineList() {
    if (this.invoice?.invoiceMedicines) {
      this.selectedMedicines = this.invoice.invoiceMedicines
        .map(item => item.medicine)
        .filter(medicine => medicine !== undefined) as Medicine[];
    } else {
      this.selectedMedicines = [];
    }
    this.medicineList = this.selectedMedicines;
    this.updateSelectedMedicineItems(this.invoice?.invoiceMedicines);
  }

  updateSelectedMedicineItems(medicines: any) {
    this.removeUnselectedMedicineItems(medicines);
    this.addNewMedicineItems(medicines);
  }
  
  removeUnselectedMedicineItems(medicines: Medicine[]) {
    const selectedMedicineItemsFormArray = this.createInvoiceForm.get('invoiceMedicines') as FormArray;
  
    for (let i = selectedMedicineItemsFormArray.length - 1; i >= 0; i--) {
      const medicineId = selectedMedicineItemsFormArray.at(i).get('medicineId')?.value;
      if (!medicines.some(medicine => medicine.id === medicineId)) {
        selectedMedicineItemsFormArray.removeAt(i);
      }
    }
  }
  
  addNewMedicineItems(medicines: Medicine[]) {
    console.log(medicines)
    const selectedMedicineItemsFormArray = this.createInvoiceForm.get('invoiceMedicines') as FormArray;
    medicines.forEach((medicine: any) => {
      const existingMedicine = selectedMedicineItemsFormArray.controls.find(control =>
        control.get('medicineId')?.value === medicine.id
      );
  
      if (!existingMedicine) {
        selectedMedicineItemsFormArray.push(this.fb.group({
          medicineName: [{value: medicine.name, disabled: true}],
          medicineId: [medicine.medicineId == null ? medicine.id : medicine.medicineId],
          dosageAmount: [medicine.dosageAmount == '' ? '' : medicine.dosageAmount, Validators.required],
          duration: [medicine.duration == '' ? '' : medicine.duration, Validators.required],
          frequency: [medicine.frequency == '' ? '' : medicine.frequency, Validators.required],
          note: [medicine.note == '' ? '' : medicine.note],
        }));
      }
    });
  }

  getDoctorServicesByDoctorId() {
    this.doctorServiceService.getDoctorServiceByDocorId(this.appointment!.doctorId.toString()).subscribe(response => {
      this.doctor_services = response;
      this.services = this.doctor_services.map(item => item.service);
      this.intializeDoctorServices();
    });
  }

  intializeDoctorServices() {
    const filteredDoctorServices = this.doctor_services!.filter(service => {
      return this.invoice?.invoiceDoctorServices.some(invoiceService => invoiceService.doctorServiceId === service.id);
    });
    this.selectedServices = filteredDoctorServices.map(item => item.service);
    const selectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;
    for (const item of this.invoice?.invoiceDoctorServices!) {
      const serviceItem = filteredDoctorServices.find(service => service.id == item.doctorServiceId)
      selectedServicesFormArray.push(this.fb.group({
        serviceName: [{ value: item.serviceName, disabled: true }, Validators.required],
        servicePrice: [{ value: item.serviceSoldPrice, disabled: true }, Validators.required],
        quantity: [item.serviceQuantity, [Validators.required, Validators.min(1)]],
        serviceId: [serviceItem?.serviceId, Validators.required],
        servicetotalPrice: [{ value: item.totalPrice, disabled: true }, Validators.required],
        serviceDisposablePrice: [item.totalDisposablesPrice],
      }));
    }
  }

  initializeCustomItems() {
    const customItemsArray = this.createInvoiceForm.get('customItems') as FormArray;

    if (this.invoice?.customItems) {
      for (const item of this.invoice.customItems) {
        customItemsArray.push(this.fb.group({
          name: [item.name, Validators.required],
          price: [item.price, [Validators.required, Validators.min(1)]],
          units: [item.units, [Validators.required, Validators.min(1)]],
        }));
      }
    }
  }

  initializeForm() {
    this.createInvoiceForm = this.fb.group({
      invoiceSelectedServices: this.fb.array([]),
      customItems: this.fb.array([]),
      totalPrice: [{ value: 0, disabled: true }, [Validators.required]],
      totalPaid: [0, [Validators.required]],
      totalRemaning: [{ value: 0, disabled: true }, [Validators.required]],
      totalAfterDiscount: [{ value: 0, disabled: true }, [Validators.required]],
      discountPercentage: [0, [Validators.required, Validators.max(100), Validators.min(0)]],
      paymentMethod: ['cash', Validators.required],
      invoiceMedicines: this.fb.array([])
    });
    this.initializeCustomItems();
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

  calculateTotals() {
    this.resetTotals();
    const selectedServicesFormArray = this.createInvoiceForm.get('invoiceSelectedServices') as FormArray;
    const selectedCustomItemsFormArray = this.createInvoiceForm.get('customItems') as FormArray;
    if (selectedServicesFormArray.length == 0 && selectedCustomItemsFormArray.length == 0) {
      this.validationErrors.push("Please Add Services Or Items To Appointment Before Calculating Totals")
    }
    else {
      this.appendTypePriceToTotal();
      if (selectedServicesFormArray.length > 0) this.calculateServiceTotals(selectedServicesFormArray);
      if (selectedCustomItemsFormArray.length > 0) this.calculateCustomItemsTotals(selectedCustomItemsFormArray);
    }
  }

  appendTypePriceToTotal() {
    var price = this.invoice?.appointmentTypePrice;
    var total = this.createInvoiceForm.get('totalPrice')?.value + price;
    this.setTotalPrice(total);
  }

  resetTotals() {
    this.setTotalPrice(0);
    this.createInvoiceForm.get('discountPercentage')?.setValue(0);
    this.createInvoiceForm.get('totalPaid')?.setValue(0);
  }

  calculateServiceTotals(selectedServicesFormArray: FormArray) {
    // Calculate the total servicetotalPrice
    const totalServicetotalPrice = selectedServicesFormArray.controls.reduce((total, control) => {
      const servicetotalPrice = control.get('servicetotalPrice')?.value;
      return total + servicetotalPrice;
    }, 0);

    // Update the totalPrice field
    var total = this.createInvoiceForm.get('totalPrice')?.value + totalServicetotalPrice;
    this.setTotalPrice(total);
  }

  calculateCustomItemsTotals(customItemsFormArray: FormArray) {
    // Calculate the total customItemstotalPrice
    const totalCustomItemstotalPrice = customItemsFormArray.controls.reduce((total, control) => {
      const price = control.get('price')?.value;
      const units = control.get('units')?.value;

      return total + (price * units);
    }, 0);

    // Update the totalPrice field
    var total = this.createInvoiceForm.get('totalPrice')?.value + totalCustomItemstotalPrice;
    this.setTotalPrice(total);
  }

  changeTotalAfterDiscount(event: any) {
    const discountValue = event.target.value;
    this.setTotalAfterDiscount(discountValue);
    this.setRemaningTotal(this.createInvoiceForm.get('totalPaid')?.value)
  }

  setTotalAfterDiscount(discountValue: number) {
    this.createInvoiceForm.get('totalAfterDiscount')?.setValue(this.createInvoiceForm.get('totalPrice')?.value * (1 - (discountValue / 100)));
  }

  changeTotalRemaning(event: any) {
    this.setRemaningTotal(event.target.value);
  }

  setRemaningTotal(totalPaid: number) {
    this.createInvoiceForm.get('totalRemaning')?.setValue(this.createInvoiceForm.get('totalAfterDiscount')?.value - totalPaid)
  }

  setTotalPrice(price: number) {
    this.createInvoiceForm.get('totalPrice')?.setValue(price);
    this.createInvoiceForm.get('totalAfterDiscount')?.setValue(price);
    this.createInvoiceForm.get('totalRemaning')?.setValue(price);
  }

  mapInvoiceFormToCreateInvoice() {
    const createInvoice: CreateInvoice = {
      id: this.invoice?.id,
      appointmentId: this.appointment?.id!,
      paymentMethod: this.createInvoiceForm.get('paymentMethod')?.value,
      discountPercentage: this.createInvoiceForm.get('discountPercentage')?.value,
      totalPaid: this.createInvoiceForm.get('totalPaid')?.value,
      invoiceDoctorServices: this.mapCreateDoctorService(),
      customItems: this.createInvoiceForm.get('customItems')?.value,
      invoiceMedicines: this.createInvoiceForm.get('invoiceMedicines')?.value
    };
    return createInvoice;
  }

  createInvoice() {
    var invoice = this.mapInvoiceFormToCreateInvoice();
    this.invoiceService.updateInvoice(invoice).subscribe(response => {
      this.sendToInvoiceView(response);
    })
  }

  sendToInvoiceView(response: Invoice) {
    const queryParams: any = { invoice: JSON.stringify(response) };
    const navigationExtras: NavigationExtras = {
      queryParams
    };
    this.router.navigate(['appointments/view-invoice/' + response.id], navigationExtras);
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
