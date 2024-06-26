import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';
import { AddAppointmentComponent } from './add-appointment/add-appointment.component';
import { UpdateAppointmentComponent } from './update-appointment/update-appointment.component';
import { AppointmentDetailedResolver } from 'src/app/core/resolvers/appointment-detailed.resolver';
import { FinalizeAppointmentComponent } from './finalize-appointment/finalize-appointment.component';
import { InvoiceAppointmentComponent } from './invoice-appointment/invoice-appointment.component';
import { InvoiceDetailedResolver } from 'src/app/core/resolvers/invoice-detailed.resolver';
import { MedicalOperationsComponent } from './medical-operations/medical-operations.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardAppointmentsComponent,
    data: {
      title: `Appointments`
    }
  },
  {
    path: 'add',
    component: AddAppointmentComponent,
    data: {
      title: `Add Appointment`
    }
  },
  {
    path: 'update/:id',
    component: UpdateAppointmentComponent,
    resolve: { appointment: AppointmentDetailedResolver },
    data: {
      title: `Update Appointment`
    }
  },
  {
    path: 'finalize/:invoiceId',
    component: FinalizeAppointmentComponent,
    resolve: { invoice: InvoiceDetailedResolver },
  },
  {
    path: 'medical-operations/:id',
    component: MedicalOperationsComponent,
    resolve: { appointment: AppointmentDetailedResolver },
    data: {
      title: `Enter Medical Operations`
    }
  },
  {
    path: 'view-invoice/:invoiceId',
    component: InvoiceAppointmentComponent,
    resolve: { invoice: InvoiceDetailedResolver },
    data: {
      title: `Invoice Details`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardAppointmentRoutingModule {
}
