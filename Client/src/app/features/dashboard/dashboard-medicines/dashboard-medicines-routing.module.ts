import { RouterModule, Routes } from "@angular/router";
import { DashboardMedicinesComponent } from "./dashboard-medicines.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    {
        path: '',
        component: DashboardMedicinesComponent,
        data: {
            title: `Medicines`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardMedicinesRoutingModule {
}