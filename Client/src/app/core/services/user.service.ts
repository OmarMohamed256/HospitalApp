import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from 'src/app/models/userData';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserData[]>(this.baseUrl + 'user');
  }
  
  getusersByRole(roleName: string) {
    return this.http.get<UserData[]>(this.baseUrl + 'user/' + roleName);
  }
}
