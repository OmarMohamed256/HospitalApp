import { AlertModule, ContainerComponent, FormSelectDirective, InputGroupTextDirective, ModalModule, NavbarModule, PaginationModule, SharedModule, TableModule } from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonSharedModule } from "src/app/shared/common-shared.module";
import { DashboardRoomsComponent } from "./dashboard-rooms.component";
import { DashboardRoomsRoutingModule } from "./dashboard-rooms-routing.module";
import { RoomsModalComponent } from './rooms-modal/rooms-modal.component';
import { NgSelectModule } from "@ng-select/ng-select";


@NgModule({
  imports: [
    DashboardRoomsRoutingModule,
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
  declarations: [DashboardRoomsComponent, RoomsModalComponent]
})
export class DashboardRoomsModule {
}
