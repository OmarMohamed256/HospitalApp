import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { ROLES } from 'src/app/constants/roles';
import { AccountService } from 'src/app/core/services/account.service';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { Invoice } from 'src/app/models/InvoiceModels/invoice';
import { User } from 'src/app/models/UserModels/user';

@Component({
  selector: 'app-invoice-appointment',
  templateUrl: './invoice-appointment.component.html',
  styleUrls: ['./invoice-appointment.component.scss']
})
export class InvoiceAppointmentComponent implements OnInit {
  invoice?: Invoice;
  currentUser?: User;
  totalPaid: number = 0;
  totalRemaining: number = 0;

  constructor(private route: ActivatedRoute, private accountService: AccountService,
    private invoiceService: InvoiceService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.invoice = data['invoice'];
      this.totalPaid = this.invoice?.totalPaid || 0;
      this.totalRemaining = this.invoice?.totalRemaining || 0;
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
    })
  }

  isDoctor(){
    return this.currentUser?.roles.includes(ROLES.DOCTOR);
  }

  updateTotalPaid() {
    this.invoiceService.updateInvoiceDebt(this.invoice?.id!, this.totalPaid).subscribe(response => {
      this.totalRemaining = response;
      this.toastr.success("Debt Updated Successfully")
    })
  }

}
