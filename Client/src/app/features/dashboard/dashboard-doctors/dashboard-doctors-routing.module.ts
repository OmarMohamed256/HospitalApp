import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardDoctorsComponent } from './dashboard-doctors.component';
import { DoctorInfoComponent } from './doctor-info/doctor-info.component';
import { UserDetailedResolver } from 'src/app/core/resolvers/user-detailed.resolver';


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
    resolve: { user: UserDetailedResolver },
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
