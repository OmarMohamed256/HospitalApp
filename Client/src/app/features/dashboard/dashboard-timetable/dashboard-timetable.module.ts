import { AlertModule, NavbarModule, PaginationModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardTimeTableRoutingModule } from "./dashboard-timetable-routing.module";
import { DashboardTimetableComponent } from "./dashboard-timetable.component";

@NgModule({
  imports: [
    DashboardTimeTableRoutingModule,
    NavbarModule,
    CommonModule,
    PaginationModule,
    CommonSharedModule,
    AlertModule
    ],
  declarations: [DashboardTimetableComponent]
})
export class DashboardTimeTableModule {
}
