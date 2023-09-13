import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardPatientsComponent } from './dashboard-patients.component';
import { PatientInfoComponent } from './patient-info/patient-info.component';
import { UserDetailedResolver } from 'src/app/core/resolvers/user-detailed.resolver';


const routes: Routes = [
  {
    path: '',
    component: DashboardPatientsComponent,
    data: {
      title: `Patient`
    }
  },
  {
    path:':id',
    component: PatientInfoComponent,
    resolve: { user: UserDetailedResolver },
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
