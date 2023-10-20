import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateInvoice } from 'src/app/models/createInvoice';
import { Invoice } from 'src/app/models/invoice';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createInvoice(createInvoice: CreateInvoice) {
    return this.http.post<Invoice>(this.baseUrl + 'invoice/', createInvoice);
  }
  updateInvoice(createInvoice: CreateInvoice) {
    return this.http.put<Invoice>(this.baseUrl + 'invoice/', createInvoice);
  }
  getInvoice(invoiceId: number) {
    return this.http.get<Invoice>(this.baseUrl + 'invoice/' + invoiceId); 
  }
}
