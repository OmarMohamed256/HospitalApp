import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { DashboardInventoryComponent } from "./dashboard-inventory.component";

const routes: Routes = [
    {
        path: '',
        component: DashboardInventoryComponent,
        data: {
            title: `Inventory`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardInventoryRoutingModule {
}