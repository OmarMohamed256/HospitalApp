import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardDoctorsComponent } from './dashboard-doctors.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardDoctorsComponent,
    data: {
      title: `Doctor`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardDoctorsRoutingModule {
}
