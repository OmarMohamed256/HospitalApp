import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { NgSelectModule } from "@ng-select/ng-select";
import { DashboardMedicinesRoutingModule } from "./dashboard-medicines-routing.module";
import { DashboardMedicinesComponent } from "./dashboard-medicines.component";
import { MedcineModalComponent } from './medcine-modal/medcine-modal.component';


@NgModule({
  imports: [
    DashboardMedicinesRoutingModule,
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
  declarations: [DashboardMedicinesComponent, MedcineModalComponent]
})
export class DashboardMedicinesModule {
}
