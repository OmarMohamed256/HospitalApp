import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GenderList } from 'src/app/constants/genders';
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
  
  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      console.log(this.user)
    })
  }

  onTabChange($event: number) {
    this.activePane = $event;
  }

}
