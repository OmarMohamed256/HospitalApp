import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ROLES } from 'src/app/constants/roles';
import { ClinicService } from 'src/app/core/services/clinic.service';
import { UserService } from 'src/app/core/services/user.service';
import { UserParams } from 'src/app/models/Params/userParams';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { UserData } from 'src/app/models/UserModels/userData';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-clinics-modal',
  templateUrl: './clinics-modal.component.html',
  styleUrls: ['./clinics-modal.component.scss']
})
export class ClinicsModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Input() specialityList: Speciality[] = [];
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() clinicAddedUpdated = new EventEmitter<Clinic>();

  createUpdateClinicForm!: FormGroup;
  roleDoctor = ROLES.DOCTOR;
  doctorParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 5,
    roleName: this.roleDoctor
  });
  doctorList: Partial<UserData>[] = [];
  selectedDoctor!: Partial<UserData>;

  constructor(private fb: FormBuilder, private clinicService: ClinicService, private userService: UserService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm() {
    this.createUpdateClinicForm = this.fb.group({
      id: [0, Validators.required],
      clinicNumber: ['', Validators.required],
      clinicSpecialityId: [0, Validators.required],
      doctorId: [0],
    });
  }
  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }
  resetDoctorList() {
    this.doctorList = [];
    this.createUpdateClinicForm.get("doctorId")?.setValue(0);

  }
  searchDoctors(event: any) {
    if (event.term.trim().length > 2) {
      this.doctorParams.searchTerm = event.term;
      this.doctorParams.doctorSpecialityId = this.createUpdateClinicForm.get('clinicSpecialityId')?.value;
      this.userService.getUserData(this.doctorParams).subscribe(response => {
        this.doctorList = response.result;
      });
    }
  }

  createUpdateClinic() {
    if (this.createUpdateClinicForm.valid) {
      if (this.createUpdateClinicForm.get("id")?.value == 0) {
        this.createClinic();
      } else {
        this.updateClinic();
      }
    }
  }

  createClinic() {
    this.clinicService.createClinic(this.createUpdateClinicForm.value).subscribe(response => {
      this.clinicAddedUpdated.emit(response);
      this.modelToggeled(false);
    })
  }

  updateClinic() {
    this.clinicService.updateClinic(this.createUpdateClinicForm.value).subscribe(response => {
      this.clinicAddedUpdated.emit(response);
      this.modelToggeled(false);
    })
  }

}
