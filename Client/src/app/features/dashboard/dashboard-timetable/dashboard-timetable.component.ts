import { Component } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { Room } from 'src/app/models/RoomModels/room';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { Pagination } from 'src/app/models/pagination';

@Component({
  selector: 'app-dashboard-timetable',
  templateUrl: './dashboard-timetable.component.html',
  styleUrls: ['./dashboard-timetable.component.scss']
})
export class DashboardTimetableComponent {
  rooms: Room[] = [];
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 6,
    includeUpcomingAppointments: true,
    roomSpecialityId: null
  }
  pagination: Pagination | null = null;

  constructor(private roomService: RoomService, private signalr: SignalrService) {
  }
  ngOnInit(): void {
    this.getRooms();
    this.signalr.startConnection();
    this.signalr.addAppointmentListner();

    // Subscribe to the appointmentFinalized event
    this.signalr.appointmentFinalized.subscribe((response) => {
      this.updateRoomAppointmentStatus(response.appointmentId, response.status);
    });
  }

  updateRoomAppointmentStatus(appointmentId: number, status: string) {
    for (const room of this.rooms) {
      if (room.doctor && room.doctor.appointments) {
        const appointmentToUpdate = room.doctor.appointments.find(appointment => appointment.id === appointmentId);
        if (appointmentToUpdate) {
          appointmentToUpdate.status = status;
          return; // Assuming each appointment has a unique ID
        }
      }
    }
  }
  getRooms() {
    this.roomService.getRooms(this.roomParams).subscribe(response => {
      this.rooms = response.result;
      this.pagination = response.pagination
    })
  }
  activePane = 0;
  onTabChange($event: number) {
    this.activePane = $event;
  }

  getColor(status: string) {
    if (status == 'finalized') return 'dark';
    return 'primary';
  }

  filterByDate(event: any) {
    this.roomParams.appointmentDateOfVisit = event.target.value;
    this.getRooms();
  }

  resetFilters() {
    this.roomParams = this.roomService.resetParams();
    this.roomParams.includeUpcomingAppointments = true;
    this.getRooms();
  }
  pageChanged(event: number) {
    this.roomParams.pageNumber = event;
    this.getRooms();
  }
}
