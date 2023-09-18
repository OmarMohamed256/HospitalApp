import { Component, OnInit } from '@angular/core';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-dashboard-specialities',
  templateUrl: './dashboard-specialities.component.html',
  styleUrls: ['./dashboard-specialities.component.scss']
})
export class DashboardSpecialitiesComponent implements OnInit {
  speciality: Speciality[] | null = [];
  constructor(private specialityService: SpecialityService) {
  }
  ngOnInit(): void {
    this.getSpecialities();
  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.speciality = response;
      console.log(this.speciality)
    })
  }
}
