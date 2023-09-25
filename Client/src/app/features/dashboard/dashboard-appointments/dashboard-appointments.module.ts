import { NgModule } from '@angular/core';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonSharedModule } from 'src/app/shared/common-shared.module';
import { DashboardAppointmentRoutingModule } from './dashboard-appointments-routing.module';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';

@NgModule({
  imports: [
    DashboardAppointmentRoutingModule,
    TableModule,
    NavbarModule,
    ContainerComponent,
    CommonModule,
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    CommonSharedModule,
    SharedModule,
    ModalModule,
    ],
  declarations: [DashboardAppointmentsComponent]
})
export class DashboardAppointmentModule {
}
