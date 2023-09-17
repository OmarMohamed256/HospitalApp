import { NgModule } from '@angular/core';
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, NavbarModule, SharedModule, TableModule } from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonSharedModule } from 'src/app/shared/common-shared.module';
import { DashboardDoctorsComponent } from './dashboard-doctors.component';
import { DashboardDoctorsRoutingModule } from './dashboard-doctors-routing.module';

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
    SharedModule
    ],
  declarations: [DashboardDoctorsComponent]
})
export class DashboardDoctorModule {
}
