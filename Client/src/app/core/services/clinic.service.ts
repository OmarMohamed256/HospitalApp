import { Injectable } from '@angular/core';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Clinic } from 'src/app/models/ClinicModels/clinic';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  baseUrl = environment.apiUrl;
  clinicParams: ClinicParams = {
    pageNumber: 1,
    pageSize: 15,
    includeUpcomingAppointments: false,
    appointmentDateOfVisit: undefined,
    clinicSpecialityId: null
  };
  clinicCache = new Map();

  constructor(private http: HttpClient) { }

  getClinics(clinicParams: ClinicParams) {
    var response = this.clinicCache.get(Object.values(clinicParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(clinicParams.pageNumber, clinicParams.pageSize);
    params = params.append('includeUpcomingAppointments', clinicParams.includeUpcomingAppointments);
    if(clinicParams.appointmentDateOfVisit)
      params = params.append('appointmentDateOfVisit', clinicParams.appointmentDateOfVisit);
    if(clinicParams.clinicSpecialityId)
      params = params.append('clinicSpecialityId', clinicParams.clinicSpecialityId);
    return getPaginatedResult<Clinic[]>(this.baseUrl + 'clinic/', params, this.http)
      .pipe(map(response => {
        this.clinicCache.set(Object.values(clinicParams).join("-"), response);
        return response;
      }));
  }

  resetParams() {
    this.clinicParams = new ClinicParams();
    this.clinicCache.clear();
    return this.clinicParams;
  }

  createClinic(clinic: Clinic) {
    return this.http.post<Clinic>(this.baseUrl + 'clinic', clinic);
  }
  
  updateClinic(clinic: Clinic) {
    return this.http.put<Clinic>(this.baseUrl + 'clinic', clinic);
  }

  deleteClinic(clinicId: number) {
    return this.http.delete(this.baseUrl + 'clinic/' + clinicId);
  }
}
