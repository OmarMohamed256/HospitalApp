import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ClinicService } from 'src/app/core/services/clinic.service';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { Pagination } from 'src/app/models/pagination';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
import { Subscription } from 'rxjs';
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

  constructor(private clinicService: ClinicService, private signalr: SignalrService) {
  }
  ngOnInit(): void {
    this.getClinics();
    this.signalr.startConnection();
    this.signalr.addAppointmentListner();
    // Subscribe to the appointmentFinalized event
    const appointmentStatusSubscription = this.signalr.appointmentStatusChanged.subscribe((response) => {
      console.log(response)
      this.clinicService.updateAppointmentStatus(response.appointmentId, response.status);
    });
    this.subscriptions.push(appointmentStatusSubscription);
    // Subscribe to the appointment status update event from your service
    const appointmentUpdateSubscription = this.clinicService.appointmentStatusUpdated$.subscribe(update => {
      if (update) {
        this.handleAppointmentStatusUpdate(update.appointmentId, update.status);
      }
    });
    this.subscriptions.push(appointmentUpdateSubscription);
  }

  handleAppointmentStatusUpdate(appointmentId: number, status: string) {
    this.getClinics(); // for simplicity, just refresh all data
  }

  getClinics() {
    this.clinicService.getClinicsWithFirstTwoUpcomingAppointments(this.clinicParams).subscribe(response => {
      this.clinics = response.result;
      this.pagination = response.pagination
    })
  }

  onTabChange($event: number) {
    this.activePane = $event;
  }

  getColor(status: string) {
    if (status == 'finalized') return 'dark';
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
