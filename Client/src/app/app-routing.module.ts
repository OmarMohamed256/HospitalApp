import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from './features/dashboard/dashboard-layout/dashboard-layout.component';
import { LoginComponent } from './features/account/login/login.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DashboardLayoutComponent,
    data: {
      title: 'Dashboard'
    },
    children: [
      {
        path: 'patients',
        loadChildren: () =>
          import('./features/dashboard/dashboard-patients/dashboard-patients.module').then((m) => m.DashboardPatientModule)
      },
      {
        path: 'services',
        loadChildren: () =>
          import('./features/dashboard/dashboard-services/dashboard-services.module').then((m) => m.DashboardServiceModule)
      },
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
