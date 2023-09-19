import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ROLES_ARRAY } from 'src/app/constants/roles';
import { DoctorWorkingHours } from 'src/app/models/doctorWorkingHours';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  createUser!: FormGroup;
  validationErrors: string[] = [];
  roles = ROLES_ARRAY;
  @Input() specialityList: Speciality[] = [];

  constructor(private fb: FormBuilder){
  }
  
  ngOnInit(): void {
    this.intializeForm();
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createUser = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      age: ['', Validators.required],
      gender: ['', Validators.required],
      fullName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      doctorSpecialityId: ['', Validators.required],
      doctorWorkingHours: this.fb.array([
        this.fb.group({
          doctorId: ['', Validators.required],
          dayOfWeek: [0, Validators.required], // 0 for Sunday
          startTime: ['', Validators.required],
          endTime: ['', Validators.required],
        }),
        // repeat for other days of the week
        this.fb.group({
          doctorId: ['', Validators.required],
          dayOfWeek: [1, Validators.required], // 1 for Monday
          startTime: ['', Validators.required],
          endTime: ['', Validators.required],
        }),
        // and so on for the rest of the days...
      ]),
      role: ['', Validators.required]
    });
  }
  logForm(){
    console.log(this.createUser)
  }
}
