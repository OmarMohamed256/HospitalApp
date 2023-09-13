import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { AdminService } from 'src/app/core/services/admin.service';
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
  genderList = [{ value: 'male', display: 'Male' }, { value: 'female', display: 'Female' }];

  constructor(private iconSetService: IconSetService, private adminService: AdminService)
  {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
    this.getPatients();
  }
  getPatients() {
    this.adminService.getusersByRole(this.userParams, 'patient').subscribe(response => {
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
    this.userParams = this.adminService.resetUserParams();
    this.getPatients();
  }

}
