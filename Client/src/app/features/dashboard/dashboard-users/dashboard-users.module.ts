import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardUsersRoutingModule } from "./dashboard-users-routing.module";
import { DashboardUsersComponent } from "./dashboard-users.component";

@NgModule({
  imports: [
    DashboardUsersRoutingModule,
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
    ModalModule
    ],
  declarations: [DashboardUsersComponent]
})
export class DashboardUsersModule {
}
