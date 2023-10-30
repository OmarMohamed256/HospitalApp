import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateInvoice } from 'src/app/models/InvoiceModels/createInvoice';
import { Invoice } from 'src/app/models/InvoiceModels/invoice';
import { environment } from 'src/environments/environment.development';
import { AppointmentService } from './appointment.service';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private appoinmentService: AppointmentService) { }

  createInvoice(createInvoice: CreateInvoice) {
    return this.http.post<Invoice>(this.baseUrl + 'invoice/', createInvoice).pipe(
      map(response => {
        this.appoinmentService.invalidateAppointmentCache();
        return response;
      })
    );
  }

  updateInvoice(createInvoice: CreateInvoice) {
    return this.http.put<Invoice>(this.baseUrl + 'invoice/', createInvoice).pipe(
      map(response => {
        this.appoinmentService.invalidateAppointmentCache();
        return response;
      })
    );
  }
  
  getInvoice(invoiceId: number) {
    return this.http.get<Invoice>(this.baseUrl + 'invoice/' + invoiceId); 
  }
  
  updateInvoiceDebt(invoiceId: number, totalPaid: number) {
    return this.http.put<number>(this.baseUrl + 'invoice/updateInvoiceDebt/' + invoiceId, totalPaid); 
  }
}
