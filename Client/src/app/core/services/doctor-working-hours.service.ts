import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DoctorWorkingHours } from 'src/app/models/UserModels/doctorWorkingHours';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class DoctorWorkingHoursService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDoctorWorkingHoursByDoctorId(doctorId: string) {
    return this.http.get<DoctorWorkingHours[]>(this.baseUrl + 'doctorWorkingHours/' + doctorId);
  }
}
