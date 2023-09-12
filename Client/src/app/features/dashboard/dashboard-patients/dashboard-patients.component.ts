import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { UserService } from 'src/app/core/services/user.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { UserData } from 'src/app/models/userData';

@Component({
  selector: 'app-dashboard-patients',
  templateUrl: './dashboard-patients.component.html',
  styleUrls: ['./dashboard-patients.component.scss']
})
export class DashboardPatientsComponent implements OnInit{
  patients: UserData[] = [];
  constructor(private iconSetService: IconSetService, private userService: UserService)
  {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
    this.getPatients();
  }
  getPatients() {
    this.userService.getusersByRole('Patient').subscribe(response => {
      this.patients = response;
    })
  }
  
  
}
