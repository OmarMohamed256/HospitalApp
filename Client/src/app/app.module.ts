import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardLayoutComponent } from './features/dashboard/dashboard-layout/dashboard-layout.component';
import { DefaultHeaderComponent } from './features/dashboard/dashboard-layout/default-header/default-header.component';
import { IconSetService } from '@coreui/icons-angular';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './features/account/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { SpinnerComponent } from './shared/spinner/spinner.component';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { CommonSharedModule } from './shared/common-shared.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { DashboardMedicinesComponent } from './features/dashboard/dashboard-medicines/dashboard-medicines.component';
import { PatientAppointmentsComponent } from './features/dashboard/dashboard-patients/patient-appointments/patient-appointments.component';

const APP_CONTAINERS = [
  DefaultHeaderComponent,
  DashboardLayoutComponent
];

@NgModule({
  declarations: [AppComponent, ...APP_CONTAINERS, LoginComponent, SpinnerComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    CommonSharedModule
  ],
  providers: [
    IconSetService,
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
