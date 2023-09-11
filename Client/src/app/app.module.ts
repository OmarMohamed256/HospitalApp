import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardLayoutComponent } from './features/dashboard/dashboard-layout/dashboard-layout.component';
import {
  AvatarModule,
  BadgeModule,
  BreadcrumbModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  DropdownModule,
  FooterModule,
  FormModule,
  GridModule,
  HeaderModule,
  ListGroupModule,
  NavModule,
  ProgressModule,
  SharedModule,
  SidebarModule,
  SpinnerModule,
  TabsModule,
  ToastModule,
  UtilitiesModule
} from '@coreui/angular';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { DefaultHeaderComponent } from './features/dashboard/dashboard-layout/default-header/default-header.component';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './features/account/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { TextInputComponent } from './core/forms/text-input/text-input.component';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { SpinnerComponent } from './shared/spinner/spinner.component';

const APP_CONTAINERS = [
  DefaultHeaderComponent,
  DashboardLayoutComponent
];

@NgModule({
  declarations: [AppComponent, ...APP_CONTAINERS, LoginComponent, TextInputComponent, SpinnerComponent],

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AvatarModule,
    BreadcrumbModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    IconModule,
    NavModule,
    ButtonModule,
    UtilitiesModule,
    ButtonGroupModule,
    FormModule,
    ReactiveFormsModule,
    SidebarModule,
    SharedModule,
    TabsModule,
    ListGroupModule,
    ProgressModule,
    BadgeModule,
    ListGroupModule,
    CardModule,
    NgScrollbarModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    SpinnerModule

  ],
  providers: [
    IconSetService,
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
