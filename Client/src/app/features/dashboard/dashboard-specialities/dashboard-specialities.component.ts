import { Component, OnInit, ViewChild } from '@angular/core';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Speciality } from 'src/app/models/speciality';
import { SpecialitiesModelComponent } from './specialities-model/specialities-model.component';

@Component({
  selector: 'app-dashboard-specialities',
  templateUrl: './dashboard-specialities.component.html',
  styleUrls: ['./dashboard-specialities.component.scss']
})
export class DashboardSpecialitiesComponent implements OnInit {
  specialityList: Speciality[] | null = [];
  constructor(private specialityService: SpecialityService) {
  }
  modalVisibility: boolean = false;
  @ViewChild(SpecialitiesModelComponent) specialityModal!: SpecialitiesModelComponent;

  ngOnInit(): void {
    this.getSpecialities();
  }
  openModal() {
    this.modalVisibility = !this.modalVisibility
  }
  
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }

  specialityCreated(newSpeciality: Speciality) {
    if (newSpeciality) {
      const index = this.specialityList!.findIndex(speciality => speciality.id === newSpeciality.id);

      if (index !== -1) {
        // Service with the same ID already exists, replace it
        this.specialityList![index] = newSpeciality;
      } else {
        // Service with this ID doesn't exist, add it
        this.specialityList!.push(newSpeciality);
      }
    }
  }
  
  openModalAndSetService(speciality: Speciality) {
    this.specialityModal.speciality = speciality;
    this.specialityModal.intializeForm();
    this.openModal();
  }
}
