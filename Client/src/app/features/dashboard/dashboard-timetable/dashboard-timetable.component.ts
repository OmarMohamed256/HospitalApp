import { Component } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { Room } from 'src/app/models/RoomModels/room';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { SignalrService } from 'src/app/core/services/signalr.service';

@Component({
  selector: 'app-dashboard-timetable',
  templateUrl: './dashboard-timetable.component.html',
  styleUrls: ['./dashboard-timetable.component.scss']
})
export class DashboardTimetableComponent {
  rooms: Room[] = [];
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 15,
    includeUpcomingAppointments: true
  }
  constructor(private roomService: RoomService, private signalr: SignalrService) {
  }
  ngOnInit(): void {
    this.getRooms();
    this.signalr.startConnection();
    this.signalr.addAppointmentListner();
  }
  getRooms() {
    this.roomService.getRooms(this.roomParams).subscribe(response => {
      this.rooms = response.result;
      console.log(this.rooms)
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
    this.roomParams.AppointmentDateOfVisit = event.target.value;
    this.getRooms();
  }

  resetFilters() {
    this.roomParams = this.roomService.resetParams();
    this.roomParams.includeUpcomingAppointments = true;
    this.getRooms();
  }
}
