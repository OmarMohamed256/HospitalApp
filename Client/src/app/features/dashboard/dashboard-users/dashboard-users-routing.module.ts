import { RouterModule, Routes } from "@angular/router";
import { DashboardUsersComponent } from "./dashboard-users.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    {
        path: '',
        component: DashboardUsersComponent,
        data: {
            title: `Specialities`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardUsersRoutingModule {
}