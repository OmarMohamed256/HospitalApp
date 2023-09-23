import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { GenderList } from 'src/app/constants/genders';
import { ROLES_ARRAY } from 'src/app/constants/roles';
import { AdminService } from 'src/app/core/services/admin.service';
import { CreateUser } from 'src/app/models/createUser';
import { Speciality } from 'src/app/models/speciality';
import { TimeSpan } from 'src/app/models/timeSpan';
import { UserData } from 'src/app/models/userData';
interface Interval {
  displayValue: string;
  timeSpanValue: string;
}
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
  createUserModel: CreateUser | undefined;
  genderList = GenderList;
  @Output() userCreated = new EventEmitter<UserData>();

  constructor(private fb: FormBuilder, private adminService: AdminService) {
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
      fullName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      gender: ['', Validators.required],
      role: ['', Validators.required],
      doctorSpecialityId: [''],
      doctorWorkingHours: this.fb.array([]),
    });

    for (let i = 0; i < 7; i++) { // Assuming 7 days in a week
      const dayControl = this.fb.group({
        doctorId: [0],
        dayOfWeek: [i], // Set the day of the week (0 for Sunday, 1 for Monday, and so on)
        startTime: [''],
        endTime: [''],
      });
      const startTimeControl = dayControl.get('startTime');
      const endTimeControl = dayControl.get('endTime');

      startTimeControl?.valueChanges.subscribe((startTime) => {
        const endTime = endTimeControl?.value;
        if (startTime && !endTime) {
          endTimeControl?.setErrors({ endTimeRequired: true });
        } else {
          endTimeControl?.setErrors(null);
        }
      });
      // Push the control to the form array
      (this.createUser.controls["doctorWorkingHours"] as FormArray).push(dayControl);
    }
  }

  startTimeAndEndTimeValidator(endTime: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const startTime = control?.value;
      const endTimeValue = control?.parent?.get(endTime)?.value;
      if (startTime != '' && endTimeValue == '') {
        console.log(endTimeValue)
        return { startTimeWithoutEndTime: true };
      }
      return null;
    };
  }



  getDayOfWeekLabel(dayOfWeek: number): string {
    switch (dayOfWeek) {
      case 0: return 'Sunday';
      case 1: return 'Monday';
      case 2: return 'Tuesday';
      case 3: return 'Wednesday';
      case 4: return 'Thursday';
      case 5: return 'Friday';
      case 6: return 'Saturday';
      default: return '';
    }
  }


  getHalfHourIntervals(): Interval[] {
    const intervals: Interval[] = [];
    for (let hours = 0; hours < 24; hours++) {
      for (let minutes = 0; minutes < 60; minutes += 30) {
        const period = hours >= 12 ? 'PM' : 'AM';
        const formattedHours = (hours % 12 === 0 ? 12 : hours % 12).toString().padStart(2, '0');
        const formattedMinutes = minutes.toString().padStart(2, '0');
        const formattedTime = `${formattedHours}:${formattedMinutes} ${period}`;
        const totalMilliseconds = (hours * 60 * 60 * 1000) + (minutes * 60 * 1000);
        const timeSpanValue = new TimeSpan(totalMilliseconds).toString();
        intervals.push({ displayValue: formattedTime, timeSpanValue });
      }
    }
    return intervals;
  }

  trackByFn(index: number, item: any) {
    return item.timeSpanValue; // unique id corresponding to the item
  }

  submitCreateUserForm() {
    if (this.createUser.valid) {
      this.createUserModel = this.mapFormToCreateUser();
      this.adminService.createUser(this.createUserModel).subscribe({
        next: (response) => {
          this.modelToggeled(false);
          this.userCreated.emit(response as UserData);
        },
        error: (error) => {
          this.validationErrors = error;
        }
      });
    }
  }

  mapFormToCreateUser(): CreateUser {
    let doctorWorkingHours = this.createUser.get('doctorWorkingHours')?.value;

    // Filter out objects with empty startTime and endTime
    doctorWorkingHours = doctorWorkingHours.filter((item: any) => item.startTime !== "" && item.endTime !== "");

    const user: Partial<CreateUser> = {
      username: this.createUser.get('username')?.value,
      email: this.createUser.get('email')?.value,
      gender: this.createUser.get('gender')?.value,
      age: +this.createUser.get('age')?.value,
      fullName: this.createUser.get('fullName')?.value,
      phoneNumber: this.createUser.get('phoneNumber')?.value,
      password: this.createUser.get('password')?.value,
      doctorSpecialityId: +this.createUser.get('doctorSpecialityId')?.value,
      role: this.createUser.get('role')?.value,
    };

    if (doctorWorkingHours.length > 0) {
      user.doctorWorkingHours = doctorWorkingHours;
    }
    return user as CreateUser;
  }
}

