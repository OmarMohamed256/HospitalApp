import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChangeRole } from 'src/app/models/UserModels/changeRole';
import { CreateUser } from 'src/app/models/UserModels/createUser';
import { UpdateUser } from 'src/app/models/UserModels/updateUser';
import { environment } from 'src/environments/environment.development';
import { UserService } from './user.service';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private userService: UserService) { }

  createUser(createUser: CreateUser) {
    return this.http.post(this.baseUrl + 'admin/createUser/', createUser).pipe(
      map(response => {
        this.userService.invalidateUsersCache();
        return response;
      })
    );
  }

  updateUser(createUser: UpdateUser) {
    return this.http.put(this.baseUrl + 'admin/updateUser/', createUser).pipe(
      map(response => {
        this.userService.invalidateUsersCache();
        return response;
      })
    );;
  }

  toggleLockout(userId: string) {
    return this.http.put(this.baseUrl + 'admin/toggleLockUser/' + userId, {});
  }

  changeRole(userId: string, changeRole: ChangeRole) {
    return this.http.put(this.baseUrl + 'admin/changeUserRole/' + userId, changeRole);
  }
}
