import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AppointmentTypeList } from 'src/app/constants/appointmentTypes';
import { ROLES } from 'src/app/constants/roles';
import { AppointmentService } from 'src/app/core/services/appointment.service';
import { DoctorWorkingHoursService } from 'src/app/core/services/doctor-working-hours.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { UserService } from 'src/app/core/services/user.service';
import { Appointment } from 'src/app/models/appointment';
import { DoctorWorkingHours } from 'src/app/models/UserModels/doctorWorkingHours';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/UserModels/userData';
import { UserParams } from 'src/app/models/Params/userParams';
import { firstValueFrom } from 'rxjs';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Medicine } from 'src/app/models/medicine';
import { MedicineService } from 'src/app/core/services/medicine.service';
import { MedicineParams } from 'src/app/models/Params/medicineParams';

@Component({
  selector: 'app-update-appointment',
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.scss']
})
export class UpdateAppointmentComponent implements OnInit {
  UpdateAppointmentForm!: FormGroup;
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
  appointment?: Appointment;
  selectedPatient: UserData | null = null;
  selectedDoctor: UserData | null = null;
  medicineList: Medicine[] = [];

  rolePatient = ROLES.PATIENT;
  roleDoctor = ROLES.DOCTOR;

  selectedDay: string | null = null;
  selectedTime: Date | null = null;

  medicineParams: MedicineParams = {
    pageNumber: 1,
    pageSize: 5,
  };

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
    private appointmentService: AppointmentService, private route: ActivatedRoute, private datePipe: DatePipe,
    private toastr: ToastrService, private medicineService: MedicineService) {
  }

  ngOnInit(): void {
    this.getSpecialities();
    this.route.data.subscribe(data => {
      this.appointment = data['appointment'];
      this.patientList = [...this.patientList, this.appointment?.patient!];
      this.selectedPatient = this.patientList[0];

      this.doctorList = [...this.doctorList, this.appointment?.doctor!];
      this.selectedDoctor = this.doctorList[0];

      this.initializeForm();
      this.nextSevenDays = this.generateDays();

      this.onDoctorSelect(this.selectedDoctor, false);
      this.selectedDay = this.getIntialSelectedDay();
      this.selectedTime = this.getIntialSelectedTime();
    })
  }

  getIntialSelectedDay() {
    if (this.appointment?.dateOfVisit) {
      const date = new Date(this.appointment.dateOfVisit);
      return date.toDateString();
    }
    return null
  }

  getIntialSelectedTime() {
    if (this.appointment?.dateOfVisit) {
      return new Date(this.appointment.dateOfVisit);
    }
    return null
  }


  initializeForm() {
    this.UpdateAppointmentForm = this.fb.group({
      id: [this.appointment?.id, Validators.required],
      type: [this.appointment?.type, Validators.required],
      status: [this.appointment?.status, Validators.required],
      dateOfVisit: [this.appointment?.dateOfVisit, Validators.required],
      creationNote: [this.appointment?.creationNote],
      doctorId: [this.appointment?.doctorId, Validators.required],
      patientId: [this.appointment?.patientId, Validators.required],
      appointmentSpecialityId: [this.appointment?.appointmentSpecialityId, Validators.required],
      appointmentMedicines: this.fb.array([])
    });
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
    const selectedMedicineItemsFormArray = this.UpdateAppointmentForm.get('appointmentMedicines') as FormArray;
    selectedMedicineItemsFormArray.clear();

    medicines.forEach((medicine: any) => {
      selectedMedicineItemsFormArray.push(this.fb.group({
        medicineId: [medicine.id],
      }));
    });
  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
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
      this.doctorParams.doctorSpecialityId = this.UpdateAppointmentForm.get('appointmentSpecialityId')?.value;
      this.userService.getUserData(this.doctorParams).subscribe(response => {
        this.doctorList = response.result;
      });
    }
  }

  onPatientSelect(patient: UserData) {
    this.UpdateAppointmentForm.get('patientId')?.setValue(patient.id);
  }

  resetDateTime() {
    this.selectedDay = null;
    this.selectedTime = null;
    this.availableDays = [];
    this.availableTimesForSelectedDay = [];
  }


  async onDoctorSelect(doctor: UserData, resetDateTime: boolean = true) {
    if (resetDateTime) this.resetDateTime();
    this.UpdateAppointmentForm.get('doctorId')?.setValue(doctor.id);
    const [doctorWorkingHours, doctorBookedDates] = await Promise.all([
      firstValueFrom(this.getDoctorWorkingHours(doctor.id)),
      firstValueFrom(this.getDoctorUpcomingAppointmentsDates(doctor.id))
    ]);

    this.doctorWorkingHours = doctorWorkingHours;
    this.doctorBookedDates = doctorBookedDates.filter(date => date !== this.appointment?.dateOfVisit);
    const timeSlots = this.generateTimeSlots(this.nextSevenDays);
    this.availableTimeSlots = this.generateAvailableTimeSlots(timeSlots);
    this.getAvailableDays();
    this.getDayAvailableTimes();
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
    this.availableDays = Array.from(uniqueDays);
  }

  getDayAvailableTimes() {
    if (this.selectedDay) {
      const selectedDate = new Date(this.selectedDay);
      this.availableTimesForSelectedDay = this.availableTimeSlots.filter(slot => {
        return slot.toDateString() === selectedDate.toDateString();
      });
      const selectedTimeDate = new Date(this.selectedTime!);
      if (!this.availableTimesForSelectedDay.some(time => {
        const timeDate = new Date(time);
        return timeDate.getTime() === selectedTimeDate.getTime();
      })) {
        this.selectedTime = this.availableTimesForSelectedDay[0];
      }
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

  submitUpdateAppointmentForm() {
    if (this.UpdateAppointmentForm.valid) {
      this.UpdateAppointmentForm.get('dateOfVisit')?.setValue(this.selectDayAndTime());
      this.updateAppointment();
    }
  }

  mapFormToAppointment() {
    const appointment: Appointment = {
      id: this.UpdateAppointmentForm.get('id')?.value,
      appointmentSpecialityId: this.UpdateAppointmentForm.get('appointmentSpecialityId')?.value,
      dateOfVisit: this.UpdateAppointmentForm.get('dateOfVisit')?.value,
      doctorId: this.UpdateAppointmentForm.get('doctorId')?.value,
      patientId: this.UpdateAppointmentForm.get('patientId')?.value,
      status: this.UpdateAppointmentForm.get('status')?.value,
      type: this.UpdateAppointmentForm.get('type')?.value,
      dateCreated: this.appointment?.dateCreated,
      appointmentMedicines: this.UpdateAppointmentForm.get('appointmentMedicines')?.value
    }
    return appointment;
  }

  updateAppointment() {
    const appointment = this.mapFormToAppointment();
    this.appointmentService.updateAppointment(appointment).subscribe({
      next: (response) => {
        this.appointmentService.clearCache();
        this.toastr.success("Appointment Updated Successfully");
      },
      error: (error) => {
        this.validationErrors = error;
      }
    });
  }

}
