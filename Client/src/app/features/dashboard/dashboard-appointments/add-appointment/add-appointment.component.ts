import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { AppointmentTypeList } from 'src/app/constants/appointmentTypes';
import { ROLES } from 'src/app/constants/roles';
import { AppointmentService } from 'src/app/core/services/appointment.service';
import { DoctorWorkingHoursService } from 'src/app/core/services/doctor-working-hours.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { UserService } from 'src/app/core/services/user.service';
import { DoctorWorkingHours } from 'src/app/models/UserModels/doctorWorkingHours';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/UserModels/userData';
import { UserParams } from 'src/app/models/Params/userParams';
import { Appointment } from 'src/app/models/appointment';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.scss']
})
export class AddAppointmentComponent implements OnInit  {
  CreateAppointmentForm!: FormGroup;
  validationErrors: string[] = [];
  typeList = AppointmentTypeList;
  specialityList: Speciality[] = [];
  patientList: UserData[] = [];
  doctorList: UserData[] = [];
  doctorWorkingHours: DoctorWorkingHours[] = [];
  doctorBookedDates: Date[] = [];
  availableTimeSlots: Date[] = [];
  nextSevenDays: Date[] = [];
  availableDays: string[] = [];
  availableTimesForSelectedDay: Date[] = [];


  rolePatient = ROLES.PATIENT;
  roleDoctor = ROLES.DOCTOR;

  selectedDay: string | null = null;
  selectedTime: string | null = null;

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

  constructor(private fb: FormBuilder, private specialityService: SpecialityService,
    private userService: UserService, private doctorWorkingHoursService: DoctorWorkingHoursService,
    private appointmentService: AppointmentService, private datePipe: DatePipe, private toastr: ToastrService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this.getSpecialities();
    this.nextSevenDays = this.generateDays();
  }


  initializeForm() {
    this.CreateAppointmentForm = this.fb.group({
      type: ['', Validators.required],
      status: ['booked', Validators.required],
      dateOfVisit: ['', Validators.required],
      creationNote: [''],
      doctorId: ['', Validators.required],
      patientId: ['', Validators.required],
      appointmentSpecialityId: ['', Validators.required],
    });
  }

  searchPatients(event: any) {
    if (event.term.trim().length > 2) {
      this.patientParams.searchTerm = event.term;
      this.userService.getUserData(this.patientParams).subscribe(response => {
        this.patientList = response.result;
      });
    }
  }

  searchDoctors(event: any) {
    if (event.term.trim().length > 2) {
      this.doctorParams.searchTerm = event.term;
      this.doctorParams.doctorSpecialityId = this.CreateAppointmentForm.get('appointmentSpecialityId')?.value;
      this.userService.getUserData(this.doctorParams).subscribe(response => {
        this.doctorList = response.result;
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

  resetDateTime() {
    this.selectedDay = null;
    this.selectedTime = null;
    this.availableDays = [];
    this.availableTimesForSelectedDay = [];
  }

  async onDoctorSelect(doctor: UserData) {
    this.resetDateTime();
    this.CreateAppointmentForm.get('doctorId')?.setValue(doctor.id);
    const [doctorWorkingHours, doctorBookedDates] = await Promise.all([
      firstValueFrom(this.getDoctorWorkingHours(doctor.id)),
      firstValueFrom(this.getDoctorUpcomingAppointmentsDates(doctor.id))
    ]);

    this.doctorWorkingHours = doctorWorkingHours;
    this.doctorBookedDates = doctorBookedDates;
    const timeSlots = this.generateTimeSlots(this.nextSevenDays);
    this.availableTimeSlots = this.generateAvailableTimeSlots(timeSlots);
    this.getAvailableDays();
  }

  getDoctorWorkingHours(doctorId: string) {
    return this.doctorWorkingHoursService.getDoctorWorkingHoursByDoctorId(doctorId);
  }

  getDoctorUpcomingAppointmentsDates(doctorId: string) {
    return this.appointmentService.getDoctorUpcomingAppointmentsDates(doctorId);
  }

  generateDays() {
    const today = new Date();
    const nextSevenDays = [];

    for (let i = 0; i < 7; i++) {
      const date = new Date(today);
      date.setDate(today.getDate() + i);
      nextSevenDays.push(date);
    }
    return nextSevenDays;
  }

  generateTimeSlots(nextSevenDays: Date[]) {
    const timeSlots: Date[] = [];
    nextSevenDays.forEach((date: Date) => {
      const dayOfWeek = date.getDay();
      const workingHour = this.doctorWorkingHours.find(wh => wh.dayOfWeek === dayOfWeek);
      if (workingHour) {
        const startTime = new Date(date.toDateString() + ' ' + workingHour.startTime);
        const endTime = new Date(date.toDateString() + ' ' + workingHour.endTime);

        for (let slotTime = new Date(startTime); slotTime < endTime; slotTime.setMinutes(slotTime.getMinutes() + 30)) {
          timeSlots.push(new Date(slotTime));
        }
        
      }
    });
    return timeSlots;
  }

  generateAvailableTimeSlots(timeSlots: Date[]) {
    const bookedDates = this.doctorBookedDates.map(dateString => new Date(dateString));
  
    const availableTimeSlots = timeSlots.filter(slot => {
      return !bookedDates.some(bookedDate => 
        bookedDate.getTime() === slot.getTime()
      );
    });
  
    return availableTimeSlots;
  }

  getAvailableDays() {
    const uniqueDays = new Set<string>();
    this.availableTimeSlots.forEach(date => {
      uniqueDays.add(date.toDateString());
    });
  
    // Convert Set back to array
    this.availableDays = Array.from(uniqueDays);
  }
  getDayAvailableTimes() {
    if (this.selectedDay) {
      const selectedDate = new Date(this.selectedDay);
      this.availableTimesForSelectedDay = this.availableTimeSlots.filter(slot => {
        return slot.toDateString() === selectedDate.toDateString();
      });
    }
  }

  selectDayAndTime() {
    if (this.selectedDay && this.selectedTime) {
      const selectedDate = new Date(this.selectedDay);
      const selectedTime = new Date(this.selectedTime);
      selectedDate.setHours(selectedTime.getHours(), selectedTime.getMinutes());
      let formattedDate = this.datePipe.transform(selectedDate, 'yyyy-MM-ddTHH:mm:ss.SSSSSSS');
      return formattedDate;
    }
    return null;
  }

  submitCreateAppointmentForm() {
    this.CreateAppointmentForm.get('dateOfVisit')?.setValue(this.selectDayAndTime());
    if(this.CreateAppointmentForm.valid) {
      this.createAppointment();
    }
  }

  createAppointment() {
    const appointment = this.mapFormToAppointment();
    this.appointmentService.createAppointment(appointment).subscribe({
      next: (response) => {
        this.appointmentService.clearCache();
        this.toastr.success("Appointment Added Successfully");
        this.router.navigateByUrl("/appointments");
      },
      error: (error) => {
        this.validationErrors = error;
      }
    });
  }

  mapFormToAppointment() {
    const appointment: Appointment = {
      appointmentSpecialityId: this.CreateAppointmentForm.get('appointmentSpecialityId')?.value,
      dateOfVisit: this.CreateAppointmentForm.get('dateOfVisit')?.value,
      doctorId: this.CreateAppointmentForm.get('doctorId')?.value,
      patientId: this.CreateAppointmentForm.get('patientId')?.value,
      status: this.CreateAppointmentForm.get('status')?.value,
      type: this.CreateAppointmentForm.get('type')?.value,
    }
    return appointment;
  }

  
}