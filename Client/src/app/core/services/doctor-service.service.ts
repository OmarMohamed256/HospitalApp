import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DoctorService } from 'src/app/models/doctorService';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class DoctorServiceService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDoctorServiceByDocorId(doctorId: string) {
    return this.http.get<DoctorService[]>(this.baseUrl + 'doctorService/services-by-doctor/' + doctorId);
  }
  
  updateDoctorServiceById(doctorService: Partial<DoctorService>) {
    return this.http.put<Partial<DoctorService>>(this.baseUrl + 'doctorService', doctorService);
  }
}
