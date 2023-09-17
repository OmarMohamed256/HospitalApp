import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardDoctorsComponent } from './dashboard-doctors.component';
import { DoctorInfoComponent } from './doctor-info/doctor-info.component';


const routes: Routes = [
  {
    path: '',
    component: DashboardDoctorsComponent,
    data: {
      title: `Doctor`
    }
  },
  {
    path:':id',
    component: DoctorInfoComponent,
    data: {
      title: `Doctor Info`
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardDoctorsRoutingModule {
}
