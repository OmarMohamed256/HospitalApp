import { AlertModule, ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardSpecialitiesRoutingModule } from "./dashboard-specialities-routing.module";
import { DashboardSpecialitiesComponent } from "./dashboard-specialities.component";

@NgModule({
  imports: [
    DashboardSpecialitiesRoutingModule,
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
  declarations: [DashboardSpecialitiesComponent]
})
export class DashboardSpecialitiesModule {
}
