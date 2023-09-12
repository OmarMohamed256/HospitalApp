import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from 'src/app/models/userData';
import { UserParams } from 'src/app/models/userParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  userParams: UserParams = {
    pageNumber: 1,
    pageSize: 5,
    orderBy: 'created'
  };

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserData[]>(this.baseUrl + 'user');
  }
  
  getusersByRole(userParams: UserParams, roleName: string) {
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<UserData[]>(this.baseUrl + 'user/' + roleName , params, this.http)
      .pipe(map(response => {
        return response;
      }))
  }
  
  setUserParams(params: UserParams) {
    this.userParams = params;
  }
}
