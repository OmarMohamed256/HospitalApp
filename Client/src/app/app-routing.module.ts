import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from './features/dashboard/dashboard-layout/dashboard-layout.component';
import { LoginComponent } from './features/account/login/login.component';
import { authGuard } from './core/guards/auth.guard';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: DashboardLayoutComponent,
    data: {
      title: 'Dashboard'
    },
    canActivate: [authGuard],
    children: [
      {
        path: 'appointments',
        loadChildren: () =>
          import('./features/dashboard/dashboard-appointments/dashboard-appointments.module').then((m) => m.DashboardAppointmentModule)
      },
      {
        path: 'patients',
        loadChildren: () =>
          import('./features/dashboard/dashboard-patients/dashboard-patients.module').then((m) => m.DashboardPatientModule)
      },
      {
        path: 'doctors',
        loadChildren: () =>
          import('./features/dashboard/dashboard-doctors/dashboard-doctors.module').then((m) => m.DashboardDoctorModule)
      },
      {
        path: 'services',
        loadChildren: () =>
          import('./features/dashboard/dashboard-services/dashboard-services.module').then((m) => m.DashboardServiceModule)
      },
      {
        path: 'specialities',
        loadChildren: () =>
          import('./features/dashboard/dashboard-specialities/dashboard-specialities.module').then((m) => m.DashboardSpecialitiesModule)
      },
      {
        path: 'users',
        loadChildren: () =>
          import('./features/dashboard/dashboard-users/dashboard-users.module').then((m) => m.DashboardUsersModule)
      },
      {
        path: 'inventory',
        loadChildren: () =>
          import('./features/dashboard/dashboard-inventory/dashboard-inventory.module').then((m) => m.DashboardInventoryModule)
      },
      {
        path: 'clinics',
        loadChildren: () =>
          import('./features/dashboard/dashboard-clinics/dashboard-clinics.module').then((m) => m.DashboardClinicsModule)
      },
      {
        path: 'medicines',
        loadChildren: () =>
          import('./features/dashboard/dashboard-medicines/dashboard-medicines.module').then((m) => m.DashboardMedicinesModule)
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
