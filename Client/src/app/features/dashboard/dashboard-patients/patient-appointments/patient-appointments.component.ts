import { Component, Input, OnInit } from '@angular/core';
import { AppointmentService } from 'src/app/core/services/appointment.service';
import { Appointment } from 'src/app/models/appointment';
import { AppointmentParams } from 'src/app/models/appointmentParams';
import { UserData } from 'src/app/models/userData';

@Component({
  selector: 'app-patient-appointments',
  templateUrl: './patient-appointments.component.html',
  styleUrls: ['./patient-appointments.component.scss']
})
export class PatientAppointmentsComponent implements OnInit {
  appointments: Appointment[] | null = [];
  @Input() user: UserData | undefined;
  appointmentParams: AppointmentParams = {
    pageNumber: 1,
    pageSize: 15,
    orderBy: 'dateCreated',
    order: 'desc',
    type: '',
    specialityId: null
  };
  
  constructor(private appointmentService: AppointmentService) {
  }
  ngOnInit(): void {
    this.getAppointments();
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
  
  getAppointments() {
    this.appointmentService.getAppointmentsByUserId(this.appointmentParams, this.user?.id!).subscribe(response => {
      this.appointments = response.result;
    })
  }
}
