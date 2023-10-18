import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GenderList } from 'src/app/constants/genders';
import { ROLES, ROLES_ARRAY } from 'src/app/constants/roles';
import { AdminService } from 'src/app/core/services/admin.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { DoctorWorkingHours } from 'src/app/models/UserModels/doctorWorkingHours';
import { UpdateUser } from 'src/app/models/UserModels/updateUser';
import { UserData } from 'src/app/models/UserModels/userData';
import { Interval } from 'src/app/models/interval';
import { Speciality } from 'src/app/models/speciality';
import { TimeSpan } from 'src/app/models/timeSpan';

@Component({
  selector: 'app-update-user-view',
  templateUrl: './update-user-view.component.html',
  styleUrls: ['./update-user-view.component.scss']
})
export class UpdateUserViewComponent implements OnInit {
  @Input() user: UserData | undefined;
  @Input() userRole: string = ROLES.PATIENT;
  updateUserForm!: FormGroup;
  validationErrors: string[] = [];
  @Input() specialityList: Speciality[] = [];
  genderList = GenderList;
  roles = ROLES_ARRAY;
  @Input() doctorWorkingHours: DoctorWorkingHours[] = [];

  constructor(private fb: FormBuilder, private adminService: AdminService, private toastr: ToastrService, private specialityService: SpecialityService) {
  }

  ngOnInit(): void {
    this.intializeForm();
    this.getSpecialities();
    console.log(this.doctorWorkingHours)
  }

  intializeForm() {
    this.updateUserForm = this.fb.group({
      id: [this.user?.id, Validators.required],
      username: [this.user?.username, Validators.required],
      email: [this.user?.email, Validators.required],
      age: [this.user?.age, Validators.required],
      fullName: [this.user?.fullName, Validators.required],
      phoneNumber: [this.user?.phoneNumber, Validators.required],
      gender: [this.user?.gender, Validators.required],
      doctorSpecialityId: [this.user?.doctorSpecialityId],
      role: [this.userRole, Validators.required],
      priceVisit: [this.user?.priceVisit],
      priceRevisit: [this.user?.priceRevisit],
      doctorWorkingHours: this.fb.array([]),
    });
    for (let i = 0; i < 7; i++) { // Assuming 7 days in a week
      const dayControl = this.fb.group({
        doctorId: [this.user?.id],
        dayOfWeek: [i],
        startTime: [''],
        endTime: [''],
      });
      const startTimeControl = dayControl.get('startTime');
      const endTimeControl = dayControl.get('endTime');

      startTimeControl?.valueChanges.subscribe((startTime) => {
        const endTime = endTimeControl?.value;
        if (startTime && !endTime) {
          endTimeControl?.setErrors({ endTimeRequired: true });
        } else if (startTime && endTime && startTime >= endTime) {
          startTimeControl?.setErrors({ startTimeInvalid: true });
        }
        else {
          endTimeControl?.setErrors(null);
        }
      });
      endTimeControl?.valueChanges.subscribe((endTime) => {
        const startTime = startTimeControl?.value;
        if (startTime && endTime && startTime >= endTime) {
          endTimeControl?.setErrors({ endTimeInvalid: true });
        } else {
          endTimeControl?.setErrors(null);
        }
      });
      // Push the control to the form array
      (this.updateUserForm.controls["doctorWorkingHours"] as FormArray).push(dayControl);
    }
    this.intializeDoctorWorkingHours();

  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
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

  trackByFn(index: number, item: any) {
    return item.timeSpanValue; // unique id corresponding to the item
  }

  intializeDoctorWorkingHours() {
    const workingHours = this.updateUserForm.controls["doctorWorkingHours"] as FormArray;
    for (const updatedEntry of this.doctorWorkingHours) {
      const dayOfWeek = updatedEntry.dayOfWeek;
      const matchingEntry = workingHours.controls.find((entry) => entry.get('dayOfWeek')?.value === dayOfWeek);
      if (matchingEntry) {
        matchingEntry.get('startTime')?.setValue(this.formatDoctorWorkingHours(updatedEntry.startTime.toString()));
        matchingEntry.get('endTime')?.setValue(this.formatDoctorWorkingHours(updatedEntry.endTime.toString()));
      }
    }
  }

  formatDoctorWorkingHours(timeString: string) {
    const [hours, minutes, seconds] = timeString.split(':');
    const totalMilliseconds = (parseInt(hours) * 60 * 60 + parseInt(minutes) * 60 + parseInt(seconds)) * 1000;
    const timeSpan = new TimeSpan(totalMilliseconds);
    const formattedTimeString = timeSpan.toString();
    return formattedTimeString;
  }

  submitUpdateUserForm() {
    const user = this.mapFormToUpdateUser();
    this.adminService.updateUser(user).subscribe({
      next: (response) => {
        this.toastr.success("User Updated Successfully");
      },
      error: (error) => {
        this.validationErrors = error;
      }
    });
  }


  mapFormToUpdateUser(): UpdateUser {
    let doctorWorkingHours = this.updateUserForm.get('doctorWorkingHours')?.value;

    // Filter out objects with empty startTime and endTime
    doctorWorkingHours = doctorWorkingHours.filter((item: any) => item.startTime !== "" && item.endTime !== "");

    const user: Partial<UpdateUser> = {
      id: this.updateUserForm.get('id')?.value,
      username: this.updateUserForm.get('username')?.value,
      email: this.updateUserForm.get('email')?.value,
      gender: this.updateUserForm.get('gender')?.value,
      age: +this.updateUserForm.get('age')?.value,
      fullName: this.updateUserForm.get('fullName')?.value,
      phoneNumber: this.updateUserForm.get('phoneNumber')?.value,
      role: this.updateUserForm.get('role')?.value,
    };

    if (doctorWorkingHours.length > 0) {
      user.doctorWorkingHours = doctorWorkingHours;
      console.log(user.doctorWorkingHours)
    }
    const selectedRole = this.updateUserForm.get('role')?.value;
    if (selectedRole === 'Doctor') {
      const doctorSpecialityId = +this.updateUserForm.get('doctorSpecialityId')?.value;
      const priceVisit = +this.updateUserForm.get('priceVisit')?.value;
      const priceRevisit = +this.updateUserForm.get('priceRevisit')?.value;

      if (doctorSpecialityId && priceVisit && priceRevisit) {
        user.doctorSpecialityId = doctorSpecialityId;
        user.priceVisit = priceVisit;
        user.priceRevisit = priceRevisit;
      } else {
        // show error
      }
    }
    return user as UpdateUser;
  }

}
