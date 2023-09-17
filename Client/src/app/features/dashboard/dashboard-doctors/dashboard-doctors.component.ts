import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { UserService } from 'src/app/core/services/user.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { UserData } from 'src/app/models/userData';
import { UserParams } from 'src/app/models/userParams';
@Component({
  selector: 'app-dashboard-doctors',
  templateUrl: './dashboard-doctors.component.html',
  styleUrls: ['./dashboard-doctors.component.scss']
})
export class DashboardDoctorsComponent implements OnInit {
  doctors: UserData[] | null = [];
  userParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 15,
  });
  pagination: Pagination | null = null;
  specialityList: Speciality[] = [];

  constructor(private iconSetService: IconSetService, private userService: UserService, private specialityService: SpecialityService)
  {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
    this.getDoctors();
    this.getSpecialities();
  }
  getDoctors() {
    this.userService.getUsersByRole(this.userParams, 'doctor').subscribe(response => {
      this.doctors = response.result;
      this.pagination = response.pagination
    })
  }
  
  pageChanged(event: number) {
    this.userParams.pageNumber = event;
    this.getDoctors();
  }

  toggleOrder() {
    this.userParams.order = (this.userParams.order === 'asc') ? 'desc' : 'asc';
    this.getDoctors(); 
  }

  resetFilters() {
    this.userParams = this.userService.resetUserParams();
    this.getDoctors();
  }
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }
  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }
}
