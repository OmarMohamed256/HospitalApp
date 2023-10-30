import { Component, OnInit } from '@angular/core';
import { GenderList } from 'src/app/constants/genders';
import { ROLES } from 'src/app/constants/roles';
import { UserService } from 'src/app/core/services/user.service';
import { Pagination } from 'src/app/models/pagination';
import { UserData } from 'src/app/models/UserModels/userData';
import { UserParams } from 'src/app/models/Params/userParams';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-dashboard-patients',
  templateUrl: './dashboard-patients.component.html',
  styleUrls: ['./dashboard-patients.component.scss']
})
export class DashboardPatientsComponent implements OnInit {
  patients: UserData[] | null = [];
  roleName = ROLES.PATIENT;
  pagination: Pagination | null = null;
  genderList = GenderList;
  modalVisibility: boolean = false;
  userParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 15,
    roleName: this.roleName
  });

  constructor(private userService: UserService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getPatients();
  }
  
  getPatients() {
    this.userService.getUsers(this.userParams).subscribe(response => {
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
    this.userParams.roleName = this.roleName;
    this.getPatients();
  }

  openCreateUserModal() {
    this.modalVisibility = !this.modalVisibility
  }

  handleUserCreated(createdUser: UserData) {
    this.getPatients();
    this.toastr.success("Patient Created Successfully");
  }

}
