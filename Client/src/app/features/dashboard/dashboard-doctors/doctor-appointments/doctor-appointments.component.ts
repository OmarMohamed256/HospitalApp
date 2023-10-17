import { Component, Input, OnInit } from '@angular/core';
import { AppointmentTypeList } from 'src/app/constants/appointmentTypes';
import { AppointmentService } from 'src/app/core/services/appointment.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Appointment } from 'src/app/models/appointment';
import { AppointmentParams } from 'src/app/models/Params/appointmentParams';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/UserModels/userData';
@Component({
  selector: 'app-doctor-appointments',
  templateUrl: './doctor-appointments.component.html',
  styleUrls: ['./doctor-appointments.component.scss']
})
export class DoctorAppointmentsComponent {
  appointments: Appointment[] | null = [];
  @Input() user: UserData | undefined;
  specialityList: Speciality[] = [];
  appointmentParams: AppointmentParams = {
    pageNumber: 1,
    pageSize: 15,
    orderBy: 'dateCreated',
    order: 'desc',
    type: '',
    specialityId: null
  };
  pagination: Pagination | null = null;
  typeList = AppointmentTypeList;

  constructor(private appointmentService: AppointmentService, private specialityService: SpecialityService) {
  }

  ngOnInit(): void {
    this.getAppointments();
    this.getSpecialities();
  }

  toggleOrder(orderBy: string) {
    if (this.appointmentParams.orderBy === orderBy) {
      this.appointmentParams.order = (this.appointmentParams.order === 'asc') ? 'desc' : 'asc';
    } else {
      this.appointmentParams.orderBy = orderBy;
      this.appointmentParams.order = 'desc';
    }

    this.getAppointments();
  }

  getAppointments() {
    this.appointmentService.getAppointmentsByDoctorId(this.appointmentParams, this.user?.id!).subscribe(response => {
      this.appointments = response.result;
      this.pagination = response.pagination;
      this.appointments?.forEach(appointment => {
        appointment.doctor = this.user;
      });
    })
  }

  pageChanged(event: number) {
    this.appointmentParams.pageNumber = event;
    this.getAppointments();
  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }

  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }

  resetFilters() {
    this.appointmentParams = this.appointmentService.resetParams();
    this.getAppointments();
  }
}
