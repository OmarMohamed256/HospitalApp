import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ROLES } from 'src/app/constants/roles';
import { RoomService } from 'src/app/core/services/room.service';
import { UserService } from 'src/app/core/services/user.service';
import { UserParams } from 'src/app/models/Params/userParams';
import { Room } from 'src/app/models/RoomModels/room';
import { UserData } from 'src/app/models/UserModels/userData';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-rooms-modal',
  templateUrl: './rooms-modal.component.html',
  styleUrls: ['./rooms-modal.component.scss']
})
export class RoomsModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Input() specialityList: Speciality[] = [];
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() roomAddedUpdated = new EventEmitter<Room>();

  createUpdateRoomForm!: FormGroup;
  roleDoctor = ROLES.DOCTOR;
  doctorParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 5,
    roleName: this.roleDoctor
  });
  doctorList: Partial<UserData>[] = [];
  selectedDoctor!: Partial<UserData>;

  constructor(private fb: FormBuilder, private roomService: RoomService, private userService: UserService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm() {
    this.createUpdateRoomForm = this.fb.group({
      id: [0, Validators.required],
      roomNumber: ['', Validators.required],
      roomSpecialityId: [0, Validators.required],
      doctorId: [0],
    });
  }
  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }
  resetDoctorList() {
    this.doctorList = [];
    this.createUpdateRoomForm.get("doctorId")?.setValue(0);

  }
  searchDoctors(event: any) {
    if (event.term.trim().length > 2) {
      this.doctorParams.searchTerm = event.term;
      this.doctorParams.doctorSpecialityId = this.createUpdateRoomForm.get('roomSpecialityId')?.value;
      this.userService.getUserData(this.doctorParams).subscribe(response => {
        this.doctorList = response.result;
      });
    }
  }

  createUpdateRoom() {
    if (this.createUpdateRoomForm.valid) {
      if (this.createUpdateRoomForm.get("id")?.value == 0) {
        this.createRoom();
      } else {
        this.updateRoom();
      }
    }
  }

  createRoom() {
    this.roomService.createRoom(this.createUpdateRoomForm.value).subscribe(response => {
      this.roomAddedUpdated.emit(response);
      this.modelToggeled(false);
    })
  }

  updateRoom() {
    this.roomService.updateRoom(this.createUpdateRoomForm.value).subscribe(response => {
      this.roomAddedUpdated.emit(response);
      this.modelToggeled(false);
    })
  }

}
