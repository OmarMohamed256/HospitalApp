import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GenderList } from 'src/app/constants/genders';
import { DoctorWorkingHoursService } from 'src/app/core/services/doctor-working-hours.service';
import { DoctorWorkingHours } from 'src/app/models/UserModels/doctorWorkingHours';
import { UserData } from 'src/app/models/UserModels/userData';

@Component({
  selector: 'app-doctor-info',
  templateUrl: './doctor-info.component.html',
  styleUrls: ['./doctor-info.component.scss']
})
export class DoctorInfoComponent implements OnInit {
  user: UserData | undefined;
  activePane = 0;
  genderList = GenderList;
  updateDoctorForm!: FormGroup;
  validationErrors: string[] = [];
  doctorWorkingHours: DoctorWorkingHours[] | null = null;
  
  constructor(private route: ActivatedRoute, private doctorWorkingHoursService: DoctorWorkingHoursService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      this.getDoctorWorkingHours(this.user!.id);
    })
  }

  onTabChange($event: number) {
    this.activePane = $event;
  }

  getDoctorWorkingHours(userId: string) {
    this.doctorWorkingHoursService.getDoctorWorkingHoursByDoctorId(userId).subscribe(response => {
      this.doctorWorkingHours = response;
    })
  }

}
