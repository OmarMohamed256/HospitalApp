import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from 'src/app/models/userData';
import { UserParams } from 'src/app/models/userParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;
  userParams: UserParams = {
    pageNumber: 1,
    pageSize: 5,
    orderBy: 'date',
    order: 'asc',
    gender: '',
    searchTerm: ''
  };
  memberCache = new Map();

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserData[]>(this.baseUrl + 'user');
  }

  resetUserParams() {
    this.userParams = new UserParams();
    return this.userParams;
  }

  getusersByRole(userParams: UserParams, roleName: string) {
    var response = this.memberCache.get(Object.values(userParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('orderBy', userParams.orderBy);
    params = params.append('order', userParams.order);
    if (userParams.searchTerm.trim() !== '') params = params.append('searchTerm', userParams.searchTerm.trim());
    if (userParams.gender.trim() !== '') params = params.append('gender', userParams.gender.trim());



    return getPaginatedResult<UserData[]>(this.baseUrl + 'admin/' + roleName, params, this.http)
      .pipe(map(response => {
        this.memberCache.set(Object.values(userParams).join("-"), response);
        return response;
      }))
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }
}
