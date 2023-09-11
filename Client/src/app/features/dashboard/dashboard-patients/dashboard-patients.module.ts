import { NgModule } from '@angular/core';
import { DashboardPatientsRoutingModule } from './dashboard-patients-routing.module';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { ButtonModule, CollapseModule, ContainerComponent, DropdownModule, NavModule, NavbarModule, NavbarTextComponent, NavbarTogglerDirective, TableModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';


@NgModule({
  imports: [
    DashboardPatientsRoutingModule,
    TableModule,
    ButtonModule,
    NavbarModule,
    ContainerComponent,
    IconModule
  ],
  declarations: [DashboardPatientsComponent]
})
export class DashboardPatientModule {
}
