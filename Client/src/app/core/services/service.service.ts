import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { ServiceParams } from 'src/app/models/serviceParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Service } from 'src/app/models/service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  baseUrl = environment.apiUrl;
  serviceParams: ServiceParams = {
    pageNumber: 1,
    pageSize: 5,
    searchTerm: '',
    specialityId: null
  };
  serviceCache = new Map();

  constructor(private http: HttpClient) { }

  resetServiceParams() {
    this.serviceParams = new ServiceParams();
    return this.serviceParams;
  }

  getServices(serviceParams: ServiceParams) {
    var response = this.serviceCache.get(Object.values(serviceParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(serviceParams.pageNumber, serviceParams.pageSize);

    if (serviceParams.searchTerm.trim() !== '') params = params.append('searchTerm', serviceParams.searchTerm.trim());
    if (serviceParams.specialityId !== null) params = params.append('specialityId', serviceParams.specialityId);



    return getPaginatedResult<Service[]>(this.baseUrl + 'service/', params, this.http)
      .pipe(map(response => {
        this.serviceCache.set(Object.values(serviceParams).join("-"), response);
        return response;
      }))
  }
  setServiceParams(params: ServiceParams) {
    this.serviceParams = params;
  }
}
