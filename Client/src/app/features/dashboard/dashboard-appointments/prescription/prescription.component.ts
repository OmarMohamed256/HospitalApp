import { Component, Input } from '@angular/core';
import { Invoice } from 'src/app/models/InvoiceModels/invoice';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.scss']
})
export class PrescriptionComponent {
  @Input() invoice!: Invoice;
  getCurrentDate() {
    return new Date();
  }
}
