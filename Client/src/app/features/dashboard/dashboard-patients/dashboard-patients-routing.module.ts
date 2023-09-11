import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardPatientsComponent } from './dashboard-patients.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardPatientsComponent,
    data: {
      title: `Patient`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardPatientsRoutingModule {
}
