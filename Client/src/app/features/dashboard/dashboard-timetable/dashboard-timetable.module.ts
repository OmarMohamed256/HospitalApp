import { AlertModule, ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardTimeTableRoutingModule } from "./dashboard-timetable-routing.module";
import { DashboardTimetableComponent } from "./dashboard-timetable.component";

@NgModule({
  imports: [
    DashboardTimeTableRoutingModule,
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
    AlertModule
    ],
  declarations: [DashboardTimetableComponent]
})
export class DashboardTimeTableModule {
}
