import { Component } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { Room } from 'src/app/models/RoomModels/room';

@Component({
  selector: 'app-dashboard-rooms',
  templateUrl: './dashboard-rooms.component.html',
  styleUrls: ['./dashboard-rooms.component.scss']
})
export class DashboardRoomsComponent {
  rooms: Room[] = [];
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 15,
    includeUpcomingAppointments: false
  }
  constructor(private roomService: RoomService) {
  }
  ngOnInit(): void {
    this.getRooms();
  }
  getRooms() {
    this.roomService.getRooms(this.roomParams).subscribe(response => {
      this.rooms = response.result;
    })
  }
}
