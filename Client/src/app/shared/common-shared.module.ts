// shared.module.ts
import { NgModule } from '@angular/core';
import { TextInputComponent } from './forms/text-input/text-input.component';
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
  SidebarModule,
  TabsModule,
  SpinnerModule,
  SharedModule
} from '@coreui/angular';
import { ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [TextInputComponent],
  exports: [TextInputComponent,
    AvatarModule,
    BreadcrumbModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    IconModule,
    NavModule,
    ButtonModule,
    ButtonGroupModule,
    FormModule,
    ReactiveFormsModule,
    SidebarModule,
    TabsModule,
    ListGroupModule,
    BadgeModule,
    CardModule,
    NgScrollbarModule,
    HttpClientModule,
    SpinnerModule,
    SharedModule,
    CommonModule
  ],
  imports: [
    AvatarModule,
    BreadcrumbModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    IconModule,
    NavModule,
    ButtonModule,
    ButtonGroupModule,
    FormModule,
    ReactiveFormsModule,
    SidebarModule,
    TabsModule,
    ListGroupModule,
    BadgeModule,
    CardModule,
    NgScrollbarModule,
    HttpClientModule,
    SpinnerModule,
    SharedModule,
    CommonModule
  ]
})
export class CommonSharedModule { }
