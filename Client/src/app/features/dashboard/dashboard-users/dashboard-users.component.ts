import { Component, OnInit } from '@angular/core';
import { ROLES_ARRAY } from 'src/app/constants/roles';
import { UserService } from 'src/app/core/services/user.service';
import { Pagination } from 'src/app/models/pagination';
import { UserData } from 'src/app/models/UserModels/userData';
import { UserParams } from 'src/app/models/Params/userParams';

@Component({
  selector: 'app-dashboard-users',
  templateUrl: './dashboard-users.component.html',
  styleUrls: ['./dashboard-users.component.scss']
})
export class DashboardUsersComponent implements OnInit {
  users: UserData[] | null = [];
  userParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 15
  });
  pagination: Pagination | null = null;
  roles = ROLES_ARRAY;
  modalVisibility: boolean = false;

  constructor(private userService: UserService) {
  }
  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers(this.userParams).subscribe(response => {
      this.users = response.result;
      this.pagination = response.pagination
      console.log(this.users)
    })
  }

  pageChanged(event: number) {
    this.userParams.pageNumber = event;
    this.getUsers();
  }

  resetFilters() {
    this.userParams = this.userService.resetUserParams();
    this.getUsers();
  }
  toggleOrder() {
    this.userParams.order = (this.userParams.order === 'asc') ? 'desc' : 'asc';
    this.getUsers();
  }
  openCreateUserModal() {
    this.modalVisibility = !this.modalVisibility
  }

  handleUserCreated(createdUser: UserData) {
    this.users?.push(createdUser);
  }
}
