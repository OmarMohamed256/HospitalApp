import { Component } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { Room } from 'src/app/models/room';
import { RoomParams } from 'src/app/models/Params/roomParams';

@Component({
  selector: 'app-dashboard-timetable',
  templateUrl: './dashboard-timetable.component.html',
  styleUrls: ['./dashboard-timetable.component.scss']
})
export class DashboardTimetableComponent {
  rooms: Room[] = [];
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 15
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
  activePane = 0;
  onTabChange($event: number) {
    this.activePane = $event;
  }
}
