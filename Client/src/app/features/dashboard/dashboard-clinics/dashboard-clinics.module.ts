import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardClinicsComponent } from "./dashboard-clinics.component";
import { DashboardClinicsRoutingModule } from "./dashboard-clinics-routing.module";
import { RoomsClinicsComponent } from './clinics-modal/clinics-modal.component';
import { NgSelectModule } from "@ng-select/ng-select";


@NgModule({
  imports: [
    DashboardClinicsRoutingModule,
    TableModule,
    NavbarModule,
    ContainerComponent,
    CommonModule,
    PaginationModule,
    FormsModule,
    FormSelectDirective,
    InputGroupTextDirective,
    CommonSharedModule,
    SharedModule,
    ModalModule,
    NgSelectModule
    ],
  declarations: [DashboardClinicsComponent, RoomsClinicsComponent]
})
export class DashboardClinicsModule {
}
