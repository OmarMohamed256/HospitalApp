import { DashboardServicesComponent } from "./dashboard-services.component";
import { DashboardServicesRoutingModule } from "./dashboard-services-routing.module";
import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";

@NgModule({
  imports: [
    DashboardServicesRoutingModule,
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
    ],
  declarations: [DashboardServicesComponent]
})
export class DashboardServiceModule {
}
