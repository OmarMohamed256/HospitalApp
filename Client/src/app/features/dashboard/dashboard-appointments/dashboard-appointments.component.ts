import { Component, OnInit } from '@angular/core';
import { AppointmentTypeList } from 'src/app/constants/appointmentTypes';
import { AppointmentService } from 'src/app/core/services/appointment.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Appointment } from 'src/app/models/appointment';
import { AppointmentParams } from 'src/app/models/Params/appointmentParams';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services/account.service';
import { User } from 'src/app/models/UserModels/user';
import { take } from 'rxjs';
import { ROLES } from 'src/app/constants/roles';

@Component({
  selector: 'app-dashboard-appointments',
  templateUrl: './dashboard-appointments.component.html',
  styleUrls: ['./dashboard-appointments.component.scss']
})
export class DashboardAppointmentsComponent implements OnInit {
  appointments: Appointment[] | null = [];
  appointmentParams: AppointmentParams = {
    pageNumber: 1,
    pageSize: 15,
    orderBy: 'dateCreated',
    order: 'desc',
    type: '',
    specialityId: null
  };
  pagination: Pagination | null = null;
  specialityList: Speciality[] = [];
  typeList = AppointmentTypeList;
  currentUser?: User;

  constructor(private appointmentService: AppointmentService,
    private specialityService: SpecialityService, private router: Router, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
    this.getAppointments();
    this.getSpecialities();
  }

  getAppointments() {
    if(this.currentUser?.roles.includes(ROLES.DOCTOR)) {
      this.appointmentService.getAppointmentsByDoctorId(this.appointmentParams, this.currentUser.id.toString()).subscribe(response => {
        this.appointments = response.result;
        this.pagination = response.pagination;
        console.log(response)
      })
    } else {
      this.appointmentService.getAppointments(this.appointmentParams).subscribe(response => {
        this.appointments = response.result;
        this.pagination = response.pagination;
      })
    }
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

  toggleOrder(orderBy: string) {
    if (this.appointmentParams.orderBy === orderBy) {
      this.appointmentParams.order = (this.appointmentParams.order === 'asc') ? 'desc' : 'asc';
    } else {
      this.appointmentParams.orderBy = orderBy;
      this.appointmentParams.order = 'desc'; // Set default order to descending when changing orderBy field
    }

    this.getAppointments(); // Assuming you have a function to fetch appointments
  }
  
  resetFilters() {
    this.appointmentParams = this.appointmentService.resetParams();
    this.getAppointments();
  }

  navigateToAppointment(appointment: Appointment, event: Event) {
    event.stopPropagation();
    if (appointment.status === 'Invoiced') {
      this.router.navigate(['appointments/view-invoice', appointment.invoiceId]);
    } else if(appointment.status === 'Booked') {
      this.router.navigate(['appointments/medical-operations', appointment.id]);
    } else if (appointment.status === 'Finalized') {
      this.router.navigate(['appointments/finalize', appointment.invoiceId]);
    }
  }

  isDoctor(){
    return this.currentUser?.roles.includes(ROLES.DOCTOR);
  }
  isReceptionist(){
    return this.currentUser?.roles.some(role => role === ROLES.ADMIN || role === ROLES.RECEPTIONIST);
  }

}
