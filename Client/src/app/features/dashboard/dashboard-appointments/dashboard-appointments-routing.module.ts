import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';
import { AddAppointmentComponent } from './add-appointment/add-appointment.component';


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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardAppointmentRoutingModule {
}
