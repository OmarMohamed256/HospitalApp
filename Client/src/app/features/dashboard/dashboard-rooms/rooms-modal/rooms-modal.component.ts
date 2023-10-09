import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoomService } from 'src/app/core/services/room.service';
import { Room } from 'src/app/models/RoomModels/room';
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
  @Input() room: Room = {
    id: 0,
    roomNumber: '',
    roomSpecialityId: 0,
    doctorId: 0
  };
  createUpdateRoomForm!: FormGroup;
  constructor(private fb: FormBuilder, private roomService: RoomService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm() {
    this.createUpdateRoomForm = this.fb.group({
      id: [this.room.id, Validators.required],
      roomNumber: [this.room.roomNumber, Validators.required],
      roomSpecialityId: [this.room.roomSpecialityId, Validators.required],
      doctorId: [this.room.doctorId, Validators.required],
    });
  }
  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }
}
