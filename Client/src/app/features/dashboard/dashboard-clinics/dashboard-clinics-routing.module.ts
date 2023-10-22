import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { DashboardClinicsComponent } from "./dashboard-clinics.component";

const routes: Routes = [
    {
        path: '',
        component: DashboardClinicsComponent,
        data: {
            title: `Clinics`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardClinicsRoutingModule {
}