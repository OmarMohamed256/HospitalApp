import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';
import { AddAppointmentComponent } from './add-appointment/add-appointment.component';
import { UpdateAppointmentComponent } from './update-appointment/update-appointment.component';
import { AppointmentDetailedResolver } from 'src/app/core/resolvers/appointment-detailed.resolver';
import { FinalizeAppointmentComponent } from './finalize-appointment/finalize-appointment.component';
import { InvoiceAppointmentComponent } from './invoice-appointment/invoice-appointment.component';
import { InvoiceDetailedResolver } from 'src/app/core/resolvers/invoice-detailed.resolver';
import { AppointmentInfoComponent } from './appointment-info/appointment-info.component';


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
    path: 'info/:id',
    component: AppointmentInfoComponent,
    data: {
      title: `Appointment Info`
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
    path: 'finalize/:id',
    component: FinalizeAppointmentComponent,
    resolve: { appointment: AppointmentDetailedResolver },
    data: {
      title: `Finalize Appointment`
    }
  },
  {
    path: 'view-invoice/:id',
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
