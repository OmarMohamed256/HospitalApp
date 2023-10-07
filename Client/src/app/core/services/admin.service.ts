import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUser } from 'src/app/models/UserModels/createUser';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createUser(createUser: CreateUser) {
    return this.http.post(this.baseUrl + 'admin/createUser/', createUser);
  }
}
