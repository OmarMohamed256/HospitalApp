import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Speciality } from 'src/app/models/speciality';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SpecialityService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getSpecialities() {
    return this.http.get<Speciality[]>(this.baseUrl + 'speciality/');
  }

  createSpeciality(speciality: Speciality) {
    return this.http.post<Speciality>(this.baseUrl + 'speciality/', speciality);
  }
  updateSpeciality(speciality: Speciality) {
    return this.http.put<Speciality>(this.baseUrl + 'speciality/', speciality);
  }
}
