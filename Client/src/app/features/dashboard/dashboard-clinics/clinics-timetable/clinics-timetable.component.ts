import { Component, Input } from '@angular/core';
import { ClinicService } from 'src/app/core/services/clinic.service';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { Pagination } from 'src/app/models/pagination';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
@Component({
  selector: 'app-clinics-timetable',
  templateUrl: './clinics-timetable.component.html',
  styleUrls: ['./clinics-timetable.component.scss']
})
export class ClinicsTimetableComponent {
  clinics: Clinic[] = [];
  activePane = 0;
  clinicParams: ClinicParams = {
    pageNumber: 1,
    pageSize: 6,
  }
  pagination: Pagination | null = null;

  constructor(private clinicService: ClinicService, private signalr: SignalrService) {
  }
  ngOnInit(): void {
    this.getClinics();
    this.signalr.startConnection();
    this.signalr.addAppointmentListner();

    // Subscribe to the appointmentFinalized event
    this.signalr.appointmentFinalized.subscribe((response) => {
      // this.updateRoomAppointmentStatus(response.appointmentId, response.status);
    });
  }

  // updateRoomAppointmentStatus(appointmentId: number, status: string) {
  //   for (const clinic of this.clinics) {
  //     if (clinic.doctor && clinic.doctor.appointments) {
  //       const appointmentToUpdate = clinic.doctor.appointments.find(appointment => appointment.id === appointmentId);
  //       if (appointmentToUpdate) {
  //         appointmentToUpdate.status = status;
  //         return; // Assuming each appointment has a unique ID
  //       }
  //     }
  //   }
  // }

  getClinics() {
    this.clinicService.getClinicsWithFirstTwoUpcomingAppointments(this.clinicParams).subscribe(response => {
      this.clinics = response.result;
      this.pagination = response.pagination
      console.log(response)
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
}
