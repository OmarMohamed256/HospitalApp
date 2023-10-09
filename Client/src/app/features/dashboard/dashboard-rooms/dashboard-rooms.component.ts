import { Component } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { Room } from 'src/app/models/RoomModels/room';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';

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
    includeUpcomingAppointments: false,
    roomSpecialityId: null
  }
  modalVisibility: boolean = false;
  specialityList: Speciality[] = [];
  pagination: Pagination | null = null;

  constructor(private roomService: RoomService, private specialityService: SpecialityService) {
  }
  ngOnInit(): void {
    this.getRooms();
    this.getSpecialities();
  }
  getRooms() {
    this.roomService.getRooms(this.roomParams).subscribe(response => {
      this.rooms = response.result;
      this.pagination = response.pagination    })
  }
  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }
  resetFilters() {
    this.roomParams = this.roomService.resetParams();
    this.getRooms();
  }
  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }
  pageChanged(event: number) {
    this.roomParams.pageNumber = event;
    this.getRooms();
  }
}
