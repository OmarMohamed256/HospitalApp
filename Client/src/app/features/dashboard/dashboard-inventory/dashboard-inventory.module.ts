import { ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardInventoryRoutingModule } from "./dashboard-inventory-routing.module";
import { DashboardInventoryComponent } from "./dashboard-inventory.component";
import { SupplyOrdersComponent } from './supply-orders/supply-orders.component';

@NgModule({
  imports: [
    DashboardInventoryRoutingModule,
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
  declarations: [DashboardInventoryComponent, SupplyOrdersComponent]
})
export class DashboardInventoryModule {
}
