import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { DashboardTimetableComponent } from "./dashboard-timetable.component";

const routes: Routes = [
    {
        path: '',
        component: DashboardTimetableComponent,
        data: {
            title: `Time Table`
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardTimeTableRoutingModule {
}