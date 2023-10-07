import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from 'src/app/models/UserModels/userData';
import { UserParams } from 'src/app/models/Params/userParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  userParams: UserParams = new UserParams({
    pageNumber: 1,
    pageSize: 15,
  });
  memberCache = new Map();

  constructor(private http: HttpClient) { }

  resetUserParams() {
    this.userParams = new UserParams();
    return this.userParams;
  }

  getUserData(userParams: UserParams) {
    const cacheKey = Object.values(userParams).join("-");
    const response = this.memberCache.get(cacheKey);
    if (response) {
      return of(response);
    }
  
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('orderBy', userParams.orderBy);
    params = params.append('order', userParams.order);
    if (userParams.searchTerm.trim() !== '') params = params.append('searchTerm', userParams.searchTerm.trim());
    if (userParams.gender.trim() !== '') params = params.append('gender', userParams.gender.trim());
    if (userParams.doctorSpecialityId !== null) params = params.append('doctorSpecialityId', userParams.doctorSpecialityId);
    if (userParams.roleName.trim() !== '') params = params.append('roleName', userParams.roleName.trim());

    const url = this.baseUrl + 'user/all/';
  
    return getPaginatedResult<UserData[]>(url, params, this.http)
      .pipe(map(response => {
        this.memberCache.set(cacheKey, response);
        return response;
      }));
  }
  
  
  getUsers(userParams: UserParams) {
    return this.getUserData(userParams);
  }

  getUser(id: string) {
    const user = [... this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((userData: UserData) => userData.id == id);

    if (user) {
      return of(user);
    }
    return this.http.get<UserData>(this.baseUrl + 'user/' + id);
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  updateUser(userData: UserData) {
    return this.http.put(this.baseUrl + 'user/', userData);
  }
}
