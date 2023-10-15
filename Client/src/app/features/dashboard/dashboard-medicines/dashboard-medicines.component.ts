import { Component, OnInit, ViewChild } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { MedicineService } from 'src/app/core/services/medicine.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { MedicineParams } from 'src/app/models/Params/medicineParams';
import { Medicine } from 'src/app/models/medicine';
import { Pagination } from 'src/app/models/pagination';
import { MedcineModalComponent } from './medcine-modal/medcine-modal.component';

@Component({
  selector: 'app-dashboard-medicines',
  templateUrl: './dashboard-medicines.component.html',
  styleUrls: ['./dashboard-medicines.component.scss']
})
export class DashboardMedicinesComponent implements OnInit {
  medicines: Medicine[] | null = [];
  medicineParams: MedicineParams = {
    pageNumber: 1,
    pageSize: 15,
  };
  pagination: Pagination | null = null;
  modalVisibility: boolean = false;
  @ViewChild(MedcineModalComponent) medicineModal!: MedcineModalComponent;

  constructor(private iconSetService: IconSetService, private medicineService: MedicineService) {
    iconSetService.icons = { ...iconSubset };
  }

  ngOnInit(): void {
    this.getMedicines();
  }

  openAndReset() {
    this.medicineModal.intializeForm();
    this.modalVisibility = !this.modalVisibility
  }

  openModal() {
    this.modalVisibility = !this.modalVisibility
  }

  openModalAndSetMedicine(medicine: Medicine) {
    this.mapMedicineToForm(medicine);
    this.openModal();
  }

  mapMedicineToForm(medicine: Medicine) {
    this.medicineModal.createUpdateMedicineForm.get("id")?.setValue(medicine.id);
    this.medicineModal.createUpdateMedicineForm.get("name")?.setValue(medicine.name);
  }

  getMedicines() {
    this.medicineService.getMedicines(this.medicineParams).subscribe(response => {
      this.medicines = response.result;
      this.pagination = response.pagination
    })
  }

  pageChanged(event: number) {
    this.medicineParams.pageNumber = event;
    this.getMedicines();
  }

  resetFilters() {
    this.medicineParams = this.medicineService.resetParams();
    this.getMedicines()
  }

  deleteMedicine(medicineId: number, event: Event) {
    event.stopPropagation();
    this.medicineService.deleteMedicine(medicineId).subscribe({
      next: (response) => {
        this.medicineService.clearCache();
        this.resetFilters();
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }

  medicineCreated(medicine: Medicine) {
    this.medicineService.clearCache();
    this.resetFilters();
  }
}
