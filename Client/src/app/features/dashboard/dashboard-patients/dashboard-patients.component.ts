import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { GenderList } from 'src/app/constants/genders';
import { UserService } from 'src/app/core/services/user.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { Pagination } from 'src/app/models/pagination';
import { UserData } from 'src/app/models/userData';
import { UserParams } from 'src/app/models/userParams';
@Component({
  selector: 'app-dashboard-patients',
  templateUrl: './dashboard-patients.component.html',
  styleUrls: ['./dashboard-patients.component.scss']
})
export class DashboardPatientsComponent implements OnInit{
  patients: UserData[] | null = [];
  userParams: UserParams = {
    pageNumber: 1,
    pageSize: 15,
    orderBy: 'date',
    order: 'asc',
    gender: '',
    searchTerm: ''
  };
  pagination: Pagination | null = null;
  genderList = GenderList;

  constructor(private iconSetService: IconSetService, private userService: UserService)
  {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
    this.getPatients();
  }
  getPatients() {
    this.userService.getUsersByRole(this.userParams, 'patient').subscribe(response => {
      this.patients = response.result;
      this.pagination = response.pagination
    })
  }
  
  pageChanged(event: number) {
    this.userParams.pageNumber = event;
    this.getPatients();
  }

  toggleOrder() {
    this.userParams.order = (this.userParams.order === 'asc') ? 'desc' : 'asc';
    this.getPatients(); 
  }

  resetFilters() {
    this.userParams = this.userService.resetUserParams();
    this.getPatients();
  }

}
