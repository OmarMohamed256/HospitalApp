import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { DashboardRoomsComponent } from "./dashboard-rooms.component";

const routes: Routes = [
    {
        path: '',
        component: DashboardRoomsComponent,
        data: {
            title: `Rooms`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardRoomsRoutingModule {
}