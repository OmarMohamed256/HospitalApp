import { Component, ViewChild } from '@angular/core';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/UserModels/userData';
import { ToastrService } from 'ngx-toastr';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
import { ClinicService } from 'src/app/core/services/clinic.service';
import { ClinicsModalComponent } from './clinics-modal/clinics-modal.component';

@Component({
  selector: 'app-dashboard-clinics',
  templateUrl: './dashboard-clinics.component.html',
  styleUrls: ['./dashboard-clinics.component.scss']
})
export class DashboardClinicsComponent {
  clinics: Clinic[] = [];
  clinicParams: ClinicParams = {
    pageNumber: 1,
    pageSize: 15,
    includeUpcomingAppointments: false,
    clinicSpecialityId: null
  }
  modalVisibility: boolean = false;
  specialityList: Speciality[] = [];
  pagination: Pagination | null = null;
  @ViewChild(ClinicsModalComponent) clinicsModal!: ClinicsModalComponent;
  activePane = 0;

  constructor(private clinicService: ClinicService, private specialityService: SpecialityService, private toastr: ToastrService) {
  }
  ngOnInit(): void {
    this.getClinics();
    this.getSpecialities();
  }
  getClinics() {
    this.clinicService.getClinics(this.clinicParams).subscribe(response => {
      this.clinics = response.result;
      this.pagination = response.pagination
    })
  }
  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }
  onTabChange($event: number) {
    this.activePane = $event;
  }
  resetFiltersAndGetClinics() {
    this.resetFilters();
    this.getClinics();
  }

  resetFilters() {
    this.clinicParams = this.clinicService.resetParams();
  }
  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }
  pageChanged(event: number) {
    this.clinicParams.pageNumber = event;
    this.getClinics();
  }

  clinicAddedUpdated(clinic: Clinic) {
    this.resetFiltersAndGetClinics();
  }

  setClinicAndShowModal(clinic: Clinic) {
    this.mapClinicDoctorToUserData(clinic);
    this.mapClinicTocreateUpdateClinicForm(clinic);
    this.toggleModal();
  }

  mapClinicDoctorToUserData(clinic: Clinic) {
    const newUserData: Partial<UserData> = {
      id: clinic.doctor?.id.toString(),
      fullName: clinic.doctor?.fullName,
    };
    // Check if doctorList already contains the doctor
    if (!this.clinicsModal.doctorList.find(doctor => doctor.id === newUserData.id)) {
      this.clinicsModal.doctorList = [newUserData];
    }
  }

  mapClinicTocreateUpdateClinicForm(clinic: Clinic) {
    this.clinicsModal.createUpdateClinicForm.get("id")?.setValue(clinic.id);
    const doctorId = clinic.doctorId ? clinic.doctorId.toString() : "0";
    this.clinicsModal.createUpdateClinicForm.get("doctorId")?.setValue(doctorId);
    this.clinicsModal.createUpdateClinicForm.get("clinicNumber")?.setValue(clinic.clinicNumber);
    this.clinicsModal.createUpdateClinicForm.get("clinicSpecialityId")?.setValue(clinic.clinicSpecialityId);
  }
  
  deleteClinic(clinicId: number, event: Event) {
    event.stopPropagation();
    this.clinicService.deleteClinic(clinicId).subscribe({
      next: (response) => {
        this.clinics = this.clinics?.filter(clinic => clinic.id !== clinicId)!;
        this.toastr.success("Clinic deleted successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }
}
