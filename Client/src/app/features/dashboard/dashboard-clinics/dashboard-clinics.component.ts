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
import { ClinicDoctor } from 'src/app/models/ClinicModels/clinicDoctor';

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
  }
  modalVisibility: boolean = false;
  specialityList: Speciality[] = [];
  pagination: Pagination | null = null;
  @ViewChild(ClinicsModalComponent) clinicsModal!: ClinicsModalComponent;
  activePane = 0;

  constructor(private clinicService: ClinicService, private toastr: ToastrService) {
  }
  
  ngOnInit(): void {
    this.getClinics();
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

  onTabChange($event: number) {
    this.activePane = $event;
  }

  pageChanged(event: number) {
    this.clinicParams.pageNumber = event;
    this.getClinics();
  }

  clinicAddedUpdated(clinic: Clinic) {
    this.getClinics();
  }

  clearFormAndShowModal() {
    this.clinicsModal.intializeForm();
    this.clinicsModal.selectedDoctor = {};
    this.clinicsModal.doctorList = [];
    this.toggleModal();
  }

  setClinicAndShowModal(clinic: Clinic) {
    this.mapClinicTocreateUpdateClinicForm(clinic);
    this.toggleModal();
  }

  mapClinicTocreateUpdateClinicForm(clinic: Clinic) {
    this.clinicsModal.createUpdateClinicForm.get("id")?.setValue(clinic.id);
    this.clinicsModal.createUpdateClinicForm.get("clinicNumber")?.setValue(clinic.clinicNumber);
    if(clinic.clinicDoctors != null) {
      this.initializeClinicDoctors(clinic.clinicDoctors);
      this.clinicsModal.doctorList = this.initDoctorListFromDoctorClinic(clinic.clinicDoctors);
      this.clinicsModal.selectedDoctor = this.initDoctorListFromDoctorClinic(clinic.clinicDoctors);
    }
  }

  initializeClinicDoctors(clinicDoctors: ClinicDoctor[]) {
    const doctorClincs = clinicDoctors.map(clinicDoctor => ({ id: clinicDoctor.doctorId, clinicId: clinicDoctor.clinicId }));
    this.clinicsModal.updateClinicDoctor(doctorClincs);
  }

  initDoctorListFromDoctorClinic(doctorClincs: any) {
    return doctorClincs.map((item: any) => {
      return {
        id: item.doctorId,
        fullName: item.doctor.fullName
      }
    })
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
