import { NgModule } from '@angular/core';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonSharedModule } from 'src/app/shared/common-shared.module';
import { DashboardDoctorsComponent } from './dashboard-doctors.component';
import { DashboardDoctorsRoutingModule } from './dashboard-doctors-routing.module';
import { DoctorInfoComponent } from './doctor-info/doctor-info.component';
import { DoctorServicesComponent } from './doctor-services/doctor-services.component';

@NgModule({
  imports: [
    DashboardDoctorsRoutingModule,
    TableModule,
    NavbarModule,
    ContainerComponent,
    CommonModule,
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    CommonSharedModule,
    SharedModule,
    ModalModule
    ],
  declarations: [DashboardDoctorsComponent, DoctorInfoComponent, DoctorServicesComponent]
})
export class DashboardDoctorModule {
}
