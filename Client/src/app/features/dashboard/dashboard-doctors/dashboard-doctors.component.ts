import { Component, OnInit } from '@angular/core';
import { ROLES } from 'src/app/constants/roles';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { UserService } from 'src/app/core/services/user.service';
import { CreateUser } from 'src/app/models/createUser';
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
  roleName = ROLES.DOCTOR;
  userParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 15,
    roleName: this.roleName
  });
  pagination: Pagination | null = null;
  specialityList: Speciality[] = [];
  modalVisibility: boolean = false;

  constructor(private userService: UserService, private specialityService: SpecialityService) {
  }
  ngOnInit(): void {
    this.getDoctors();
    this.getSpecialities();
  }
  getDoctors() {
    this.userService.getUsers(this.userParams).subscribe(response => {
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
    this.userParams.roleName = this.roleName;
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
  openCreateUserModal() {
    this.modalVisibility = !this.modalVisibility
  }

  handleUserCreated(createdUser: UserData) {
    this.doctors?.push(createdUser);
  }
  
}
