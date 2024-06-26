import { Injectable } from '@angular/core';
import { ClinicParams } from 'src/app/models/Params/clinicParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Clinic } from 'src/app/models/ClinicModels/clinic';
import { ToastrService } from 'ngx-toastr';
import { AppointmentService } from './appointment.service';

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
  clinicWithAppointmentsCache = new Map();

  private appointmentStatusUpdated = new BehaviorSubject<{ appointmentId: number, status: string } | null>(null);
  appointmentStatusUpdated$ = this.appointmentStatusUpdated.asObservable();

  constructor(private http: HttpClient, private toastr: ToastrService, private appoinmentService: AppointmentService) { }

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
    var response = this.clinicWithAppointmentsCache.get(Object.values(clinicParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(clinicParams.pageNumber, clinicParams.pageSize);
    return getPaginatedResult<Clinic[]>(this.baseUrl + 'clinic/getClinicsWithFirstTwoUpcomingAppointments/', params, this.http)
      .pipe(map(response => {
        this.clinicWithAppointmentsCache.set(Object.values(clinicParams).join("-"), response);
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

  private invalidateClinicCache() {
    this.clinicCache.clear();
    this.clinicWithAppointmentsCache.clear();
  }
  updateDoctorBookedWithAppointmentsByDoctorId(doctorId: number, appointmentId: number, appointmentStatus: string) {
    // Call the service to get the updated appointments list for the specified doctor.
    this.appoinmentService.getFirstTwoUpcomingAppointmentsForDoctorById(doctorId).subscribe(appointments => {
      // If successful, go through the cache and update the appointments for the matching doctor.
      this.clinicWithAppointmentsCache.forEach((value: any, key: string) => {
        value.result.forEach((clinic: Clinic) => {
          clinic.clinicDoctors?.forEach(clinicDoctor => {
            if (clinicDoctor.doctor?.id === doctorId) {
              // Update the bookedWithAppointments array with the new data.
              clinicDoctor.doctor.bookedWithAppointments = appointments;
              this.appointmentStatusUpdated.next({appointmentId: appointmentId, status: appointmentStatus });
            }
          });
        });
      });
    });
  }

}
