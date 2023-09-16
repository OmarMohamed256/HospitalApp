import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, NavbarModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule } from '@angular/common';
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
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    CommonSharedModule,
    SharedModule
    ],
  declarations: [DashboardPatientsComponent, PatientInfoComponent, PatientAppointmentsComponent]
})
export class DashboardPatientModule {
}
