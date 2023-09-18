import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { DashboardSpecialitiesComponent } from "./dashboard-specialities.component";

const routes: Routes = [
    {
        path: '',
        component: DashboardSpecialitiesComponent,
        data: {
            title: `Specialities`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardSpecialitiesRoutingModule {
}