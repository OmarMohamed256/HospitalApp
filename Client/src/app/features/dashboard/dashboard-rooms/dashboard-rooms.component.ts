import { Component, ViewChild } from '@angular/core';
import { RoomService } from 'src/app/core/services/room.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { Room } from 'src/app/models/RoomModels/room';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { RoomsModalComponent } from './rooms-modal/rooms-modal.component';
import { RoomDoctor } from 'src/app/models/RoomModels/roomDoctor';
import { UserData } from 'src/app/models/UserModels/userData';
import { ToastrService } from 'ngx-toastr';

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
  @ViewChild(RoomsModalComponent) roomsModal!: RoomsModalComponent;

  constructor(private roomService: RoomService, private specialityService: SpecialityService, private toastr: ToastrService) {
  }
  ngOnInit(): void {
    this.getRooms();
    this.getSpecialities();
  }
  getRooms() {
    this.roomService.getRooms(this.roomParams).subscribe(response => {
      this.rooms = response.result;
      this.pagination = response.pagination
    })
  }
  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }

  resetFiltersAndGetRooms() {
    this.resetFilters();
    this.getRooms();
  }

  resetFilters() {
    this.roomParams = this.roomService.resetParams();
  }
  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }
  pageChanged(event: number) {
    this.roomParams.pageNumber = event;
    this.getRooms();
  }

  roomAddedUpdated(room: Room) {
    this.resetFiltersAndGetRooms();
  }

  setRoomAndShowModal(room: Room) {
    this.mapRoomDoctorToUserData(room);
    this.mapRoomTocreateUpdateRoomForm(room);
    this.toggleModal();
  }

  mapRoomDoctorToUserData(room: Room) {
    const newUserData: Partial<UserData> = {
      id: room.doctor?.id.toString(),
      fullName: room.doctor?.fullName,
    };
    // Check if doctorList already contains the doctor
    if (!this.roomsModal.doctorList.find(doctor => doctor.id === newUserData.id)) {
      this.roomsModal.doctorList = [newUserData];
    }
  }

  mapRoomTocreateUpdateRoomForm(room: Room) {
    this.roomsModal.createUpdateRoomForm.get("id")?.setValue(room.id);
    const doctorId = room.doctorId ? room.doctorId.toString() : "0";
    this.roomsModal.createUpdateRoomForm.get("doctorId")?.setValue(doctorId);
    this.roomsModal.createUpdateRoomForm.get("roomNumber")?.setValue(room.roomNumber);
    this.roomsModal.createUpdateRoomForm.get("roomSpecialityId")?.setValue(room.roomSpecialityId);
  }
  deleteRoom(roomId: number, event: Event) {
    event.stopPropagation();
    this.roomService.deleteRoom(roomId).subscribe({
      next: (response) => {
        this.rooms = this.rooms?.filter(room => room.id !== roomId)!;
        this.toastr.success("Room deleted successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }
}
