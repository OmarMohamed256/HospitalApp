import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { AppointmentTypeList } from 'src/app/constants/appointmentTypes';
import { ROLES } from 'src/app/constants/roles';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { UserService } from 'src/app/core/services/user.service';
import { DoctorWorkingHours } from 'src/app/models/doctorWorkingHours';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/userData';
import { UserParams } from 'src/app/models/userParams';

@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.scss']
})
export class AddAppointmentComponent implements OnInit {
  CreateAppointmentForm!: FormGroup;
  validationErrors: string[] = [];
  typeList = AppointmentTypeList;
  specialityList: Speciality[] = [];
  patientList: UserData[] = [];
  doctortList: UserData[] = [];
  doctorWorkingHours: DoctorWorkingHours[] = [];
  
  rolePatient = ROLES.PATIENT;
  roleDoctor = ROLES.DOCTOR;

  patientParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 5,
    roleName: this.rolePatient
  });

  doctorParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 5,
    roleName: this.roleDoctor
  });

  constructor(private fb: FormBuilder, private specialityService: SpecialityService, private userService: UserService) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this.getSpecialities();
  }

  initializeForm() {
    this.CreateAppointmentForm = this.fb.group({
      type: ['', Validators.required],
      status: ['booked', Validators.required],
      dateOfVisit: ['', Validators.required],
      creationNote: [''],
      drawUrl: [''],
      doctorId: ['', Validators.required],
      patientId: ['', Validators.required],
      appointmentSpecialityId: ['', Validators.required],
    });
  }

  searchPatients(event: any) {
    if(event.term.trim().length > 2)
    {
      this.patientParams.searchTerm = event.term;
      this.userService.getUserData(this.patientParams).subscribe(response => {
        this.patientList = response.result;
      });
    }
  }

  searchDoctors(event: any) {
    if(event.term.trim().length > 2)
    {
      this.doctorParams.searchTerm = event.term;
      this.doctorParams.doctorSpecialityId = this.CreateAppointmentForm.get('appointmentSpecialityId')?.value;
      this.userService.getUserData(this.doctorParams).subscribe(response => {
        this.doctortList = response.result;
      });
    }
  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }

  onPatientSelect(patient: UserData) {
    this.CreateAppointmentForm.get('patientId')?.setValue(patient.id);
  }

  onDoctorSelect(doctor: UserData) {
    this.CreateAppointmentForm.get('doctorId')?.setValue(doctor.id);
  }

}
