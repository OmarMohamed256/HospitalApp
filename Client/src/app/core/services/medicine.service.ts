import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { MedicineParams } from 'src/app/models/Params/medicineParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Medicine } from 'src/app/models/medicine';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {
  baseUrl = environment.apiUrl;
  medicineCache = new Map();
  medicineParams: MedicineParams = {
    pageNumber: 1,
    pageSize: 15,
  };
  constructor(private http: HttpClient) { }

  getMedicines(medicineParams: MedicineParams) {
    var response = this.medicineCache.get(Object.values(medicineParams).join("-"));
    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(medicineParams.pageNumber, medicineParams.pageSize);
    if (medicineParams.searchTerm && medicineParams.searchTerm.trim() !== '')
      params = params.append('searchTerm', medicineParams.searchTerm.trim());

    return getPaginatedResult<Medicine[]>(this.baseUrl + 'medicine/', params, this.http)
      .pipe(map(response => {
        this.medicineCache.set(Object.values(medicineParams).join("-"), response);
        return response;
      }))
  }
  createMedicine(medicine: Medicine) {
    return this.http.post<Medicine>(this.baseUrl + 'medicine', medicine)
  }

  updateMedicine(medicine: Medicine) {
    return this.http.put<Medicine>(this.baseUrl + 'medicine', medicine)
  }
  deleteMedicine(medicineId: number) {
    return this.http.delete(this.baseUrl + 'medicine/' + medicineId)
  }
  resetParams() {
    this.medicineParams = new MedicineParams();
    return this.medicineParams;
  }
  clearCache() {
    this.medicineCache.clear();
  }
}
