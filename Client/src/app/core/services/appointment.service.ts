import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { AppointmentParams } from 'src/app/models/appointmentParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Appointment } from 'src/app/models/appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  baseUrl = environment.apiUrl;
  appointmentCache = new Map();
  currentUserId: string = '';
  appointmentParams: AppointmentParams = {
    pageNumber: 1,
    pageSize: 15,
    orderBy: 'dateCreated',
    order: 'desc',
    type: '',
    specialityId: null
  };

  constructor(private http: HttpClient) { }

  getAppointmentsByUserId(appointmentParams: AppointmentParams, userId: string) {
    if (this.currentUserId !== userId) {
      this.appointmentCache.clear();
      this.currentUserId = userId;
    }
    var response = this.appointmentCache.get(Object.values(appointmentParams).join("-"));

    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(appointmentParams.pageNumber, appointmentParams.pageSize);
    params = params.append('orderBy', appointmentParams.orderBy);
    params = params.append('order', appointmentParams.order);

    if (appointmentParams.specialityId !== null) params = params.append('specialityId', appointmentParams.specialityId);
    if (appointmentParams.type.trim() !== '') params = params.append('type', appointmentParams.type.trim());

    return getPaginatedResult<Appointment[]>(this.baseUrl + 'appointment/' + userId, params, this.http)
      .pipe(map(response => {
        this.appointmentCache.set(Object.values(appointmentParams).join("-"), response);
        return response;
      }))
  }

  getAppointments(appointmentParams: AppointmentParams) {
    var response = this.appointmentCache.get(Object.values(appointmentParams).join("-"));

    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(appointmentParams.pageNumber, appointmentParams.pageSize);
    params = params.append('orderBy', appointmentParams.orderBy);
    params = params.append('order', appointmentParams.order);

    if (appointmentParams.specialityId !== null) params = params.append('specialityId', appointmentParams.specialityId);
    if (appointmentParams.type.trim() !== '') params = params.append('type', appointmentParams.type.trim());

    return getPaginatedResult<Appointment[]>(this.baseUrl + 'appointment/all/', params, this.http)
    .pipe(map(response => {
      this.appointmentCache.set(Object.values(appointmentParams).join("-"), response);
      return response;
    }))
  }
  resetParams() {
    this.appointmentParams = new AppointmentParams();
    return this.appointmentParams;
  }
  getDoctorUpcomingAppointmentsDates(doctorId: string) {
    return this.http.get<Date[]>(this.baseUrl + 'appointment/getUpcomingDoctorAppointmentDates/' + doctorId);
  }

  createAppointment(appointment: Appointment) {
    return this.http.post<Appointment>(this.baseUrl + 'appointment', appointment)
  }

  updateAppointment(appointment: Appointment) {
    return this.http.put<Appointment>(this.baseUrl + 'appointment', appointment)
  }

  getAppointmentById(appointmentId: string) {
    const appointment = [... this.appointmentCache.values()]
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((appointment: Appointment) => appointment.id?.toString() == appointmentId);

  if (appointment) {
    return of(appointment);
  }
  return this.http.get<Appointment>(this.baseUrl + 'appointment/getAppointmentById/' + appointmentId);
  }

  clearCache() {
    this.appointmentCache.clear
  }
}
