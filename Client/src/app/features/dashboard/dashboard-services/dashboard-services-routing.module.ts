import { RouterModule, Routes } from "@angular/router";
import { DashboardServicesComponent } from "./dashboard-services.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    {
        path: '',
        component: DashboardServicesComponent,
        data: {
            title: `Services`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardServicesRoutingModule {
}