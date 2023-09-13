import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { ButtonModule, ContainerComponent, FormSelectDirective, InputGroupTextDirective, NavModule, NavbarModule, PaginationModule, TableModule, TabsModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { FormsModule } from '@angular/forms';
import { PatientInfoComponent } from './patient-info/patient-info.component';


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
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    NavModule, 
    TabsModule,
    ],
  declarations: [DashboardPatientsComponent, PaginationComponent, PatientInfoComponent]
})
export class DashboardPatientModule {
}
