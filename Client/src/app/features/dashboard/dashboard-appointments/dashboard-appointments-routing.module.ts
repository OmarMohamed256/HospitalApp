import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';
import { AddAppointmentComponent } from './add-appointment/add-appointment.component';
import { UpdateAppointmentComponent } from './update-appointment/update-appointment.component';
import { AppointmentDetailedResolver } from 'src/app/core/resolvers/appointment-detailed.resolver';
import { FinalizeAppointmentComponent } from './finalize-appointment/finalize-appointment.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardAppointmentsComponent,
    data: {
      title: `Appointments`
    }
  },
  {
    path:'add',
    component: AddAppointmentComponent,
    data: {
      title: `Add Appointment`
    }
  },
  {
    path:'update/:id',
    component: UpdateAppointmentComponent,
    resolve: { appointment: AppointmentDetailedResolver },
    data: {
      title: `Update Appointment`
    }
  },
  {
    path:'finalize/:id',
    component: FinalizeAppointmentComponent,
    data: {
      title: `Finalize Appointment`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardAppointmentRoutingModule {
}
