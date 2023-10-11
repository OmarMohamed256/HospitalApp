import { AlertModule, ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { NgSelectModule } from "@ng-select/ng-select";

import { DashboardTimeTableRoutingModule } from "./dashboard-timetable-routing.module";
import { DashboardTimetableComponent } from "./dashboard-timetable.component";

@NgModule({
  imports: [
    DashboardTimeTableRoutingModule,
    NavbarModule,
    CommonModule,
    PaginationModule,
    FormsModule,
    CommonSharedModule,
    SharedModule,
    AlertModule,
    ],
  declarations: [DashboardTimetableComponent]
})
export class DashboardTimeTableModule {
}
