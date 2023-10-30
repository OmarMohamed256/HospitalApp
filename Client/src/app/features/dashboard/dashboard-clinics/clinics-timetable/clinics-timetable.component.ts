import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ClinicService } from 'src/app/core/services/clinic.service';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { Pagination } from 'src/app/models/pagination';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
import { Subscription } from 'rxjs';
import { Appointment } from 'src/app/models/appointment';
@Component({
  selector: 'app-clinics-timetable',
  templateUrl: './clinics-timetable.component.html',
  styleUrls: ['./clinics-timetable.component.scss']
})
export class ClinicsTimetableComponent implements OnInit, OnDestroy {
  clinics: Clinic[] = [];
  activePane = 0;
  clinicParams: ClinicParams = {
    pageNumber: 1,
    pageSize: 6,
  }
  pagination: Pagination | null = null;
  private subscriptions: Subscription[] = [];  // To hold various subscriptions
  appointmentList: Appointment[] = [];

  constructor(private clinicService: ClinicService, private signalr: SignalrService) {
  }
  ngOnInit(): void {
    this.getClinics();
    this.signalr.startConnection();
    this.signalr.addAppointmentListner();

    // Subscribe to the appointmentFinalized event
    const appointmentStatusSubscription = this.signalr.appointmentStatusChanged.subscribe((response) => {
      const doctorId = this.getDoctorIdByAppointmentId(response.appointmentId);
      this.clinicService.updateDoctorBookedWithAppointmentsByDoctorId(doctorId, response.appointmentId, response.status);
    });
    this.subscriptions.push(appointmentStatusSubscription);

    // Subscribe to the appointment status update event from your service
    const appointmentUpdateSubscription = this.clinicService.appointmentStatusUpdated$.subscribe(update => {
        if(update) {
          this.handleAppointmentStatusUpdate();
        }
    });
    this.subscriptions.push(appointmentUpdateSubscription);
  }

  handleAppointmentStatusUpdate() {
    this.getClinics(); // for simplicity, just refresh all data
  }

  getClinics() {
    this.clinicService.getClinicsWithFirstTwoUpcomingAppointments(this.clinicParams).subscribe(response => {
      this.clinics = response.result;
      this.pagination = response.pagination;
      this.getClinicsUpcomingAppointments();
    })
  }

  getClinicsUpcomingAppointments() {
    this.clinics.forEach(clinic => {
      clinic.upcomingAppointments = this.getUpcomingAppointments(clinic);
    });
  }

  getDoctorIdByAppointmentId(appointmentId: number): number {
    // Find the appointment object by appointmentId
    for (const clinic of this.clinics) {
      if (clinic.upcomingAppointments) {
        const appointment = clinic.upcomingAppointments.find(item => item.id === appointmentId);
        if (appointment) {
          return appointment.doctorId;
        }
      }
    }
    return 0;
  }

  getUpcomingAppointments(clinic: Clinic): Appointment[] {
    // Flatten the array of appointments
    if (clinic.clinicDoctors) {
      const allAppointments: Appointment[] = clinic.clinicDoctors.flatMap(doctorInfo =>
        doctorInfo.doctor?.bookedWithAppointments ?? []  // if bookedWithAppointments is undefined, use an empty array
      );

      // Sort the appointments by date in ascending order
      const sortedAppointments = allAppointments.sort((a, b) =>
        new Date(a.dateOfVisit).getTime() - new Date(b.dateOfVisit).getTime()
      );

      // Get the first two appointments
      const upcomingAppointments = sortedAppointments.slice(0, 2);
      return upcomingAppointments;
    }
    return [];
  }

  onTabChange($event: number) {
    this.activePane = $event;
  }

  getColor(status: string) {
    if (status == 'Finalized') return 'dark';
    return 'primary';
  }
  pageChanged(event: number) {
    this.clinicParams.pageNumber = event;
    this.getClinics();
  }
  ngOnDestroy() {
    // Clean up subscriptions when the component is destroyed
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }
}
