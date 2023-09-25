import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardAppointmentsComponent,
    data: {
      title: `Appointments`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardAppointmentRoutingModule {
}
