import { NgModule } from '@angular/core';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonSharedModule } from 'src/app/shared/common-shared.module';
import { DashboardAppointmentRoutingModule } from './dashboard-appointments-routing.module';
import { DashboardAppointmentsComponent } from './dashboard-appointments.component';
import { AddAppointmentComponent } from './add-appointment/add-appointment.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { UpdateAppointmentComponent } from './update-appointment/update-appointment.component';
import { FinalizeAppointmentComponent } from './finalize-appointment/finalize-appointment.component';
import { InvoiceAppointmentComponent } from './invoice-appointment/invoice-appointment.component';
import { PrescriptionComponent } from './prescription/prescription.component';
import { NgxPrintModule } from 'ngx-print';
import { MedicalOperationsComponent } from './medical-operations/medical-operations.component';

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
    NgSelectModule,
    NgxPrintModule
  ],
  declarations: [DashboardAppointmentsComponent, AddAppointmentComponent, UpdateAppointmentComponent, FinalizeAppointmentComponent, InvoiceAppointmentComponent, PrescriptionComponent, MedicalOperationsComponent],
  providers: [DatePipe]
})
export class DashboardAppointmentModule {
}
