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
  };
  clinicCache = new Map();

  constructor(private http: HttpClient) { }

  getClinics(clinicParams: ClinicParams) {
    var response = this.clinicCache.get(Object.values(clinicParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(clinicParams.pageNumber, clinicParams.pageSize);
    return getPaginatedResult<Clinic[]>(this.baseUrl + 'clinic/', params, this.http)
      .pipe(map(response => {
        this.clinicCache.set(Object.values(clinicParams).join("-"), response);
        return response;
      }));
  }

  getClinicsWithFirstTwoUpcomingAppointments(clinicParams: ClinicParams) {
    var response = this.clinicCache.get(Object.values(clinicParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(clinicParams.pageNumber, clinicParams.pageSize);
    return getPaginatedResult<Clinic[]>(this.baseUrl + 'clinic/getClinicsWithFirstTwoUpcomingAppointments/', params, this.http)
      .pipe(map(response => {
        this.clinicCache.set(Object.values(clinicParams).join("-"), response);
        return response;
      }));
  }

  createClinic(clinic: Clinic) {
    return this.http.post<Clinic>(this.baseUrl + 'clinic', clinic).pipe(
      map(response => {
        // After creating a clinic, we should invalidate the cache since we don't know the exact page where it will appear (especially if sorted).
        this.invalidateClinicCache();
        return response;
      })
    );
  }
  
  updateClinic(clinic: Clinic) {
    return this.http.put<Clinic>(this.baseUrl + 'clinic', clinic).pipe(
      map(response => {
        // After updating a clinic, it's safe to invalidate the cache to reflect the changes (as the clinic might shift pages).
        this.invalidateClinicCache();
        return response;
      })
    );
  }
  
  deleteClinic(clinicId: number) {
    return this.http.delete(this.baseUrl + 'clinic/' + clinicId).pipe(
      map(response => {
        // After deleting, the clinic is removed, potentially affecting the listing order on all pages.
        this.invalidateClinicCache();
        return response;
      })
    );
  }
  
  // Method to invalidate the entire clinic cache.
  private invalidateClinicCache() {
    // Since the structure is based on pageNumber and pageSize, and any new addition, update or deletion can alter the pages,
    // it's safest to clear the entire cache.
    this.clinicCache.clear();
  }
  
}
