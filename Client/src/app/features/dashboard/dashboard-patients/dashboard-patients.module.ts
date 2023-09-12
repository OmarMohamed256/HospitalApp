import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { ButtonModule, ContainerComponent, NavbarModule, PaginationModule, TableModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  imports: [
    DashboardPatientsRoutingModule,
    TableModule,
    ButtonModule,
    NavbarModule,
    ContainerComponent,
    IconModule,
    CommonModule,
    PaginationModule,
    FormsModule
  ],
  declarations: [DashboardPatientsComponent, PaginationComponent]
})
export class DashboardPatientModule {
}
