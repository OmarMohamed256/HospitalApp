import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { Room } from 'src/app/models/room';
import { RoomParams } from 'src/app/models/roomParams';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit{
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

}
