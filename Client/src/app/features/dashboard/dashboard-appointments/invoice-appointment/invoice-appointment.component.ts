import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Invoice } from 'src/app/models/invoice';

@Component({
  selector: 'app-invoice-appointment',
  templateUrl: './invoice-appointment.component.html',
  styleUrls: ['./invoice-appointment.component.scss']
})
export class InvoiceAppointmentComponent implements OnInit {
  invoice?: Invoice;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.invoice = data['invoice'];
    })
  }

}
