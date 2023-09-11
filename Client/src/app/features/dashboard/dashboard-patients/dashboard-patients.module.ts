import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { TableModule } from '@coreui/angular';


@NgModule({
  imports: [
    DashboardPatientsRoutingModule,
    TableModule
  ],
  declarations: [DashboardPatientsComponent]
})
export class DashboardPatientModule {
}
