import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, NavbarModule, PaginationModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { FormsModule } from '@angular/forms';
import { PatientInfoComponent } from './patient-info/patient-info.component';
import { CommonSharedModule } from 'src/app/shared/common-shared.module';
import { PatientAppointmentsComponent } from './patient-appointments/patient-appointments.component';


@NgModule({
  imports: [
    DashboardPatientsRoutingModule,
    TableModule,
    NavbarModule,
    ContainerComponent,
    CommonModule,
    PaginationModule,
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    CommonSharedModule,
    SharedModule
    ],
  declarations: [DashboardPatientsComponent, PaginationComponent, PatientInfoComponent, PatientAppointmentsComponent]
})
export class DashboardPatientModule {
}
