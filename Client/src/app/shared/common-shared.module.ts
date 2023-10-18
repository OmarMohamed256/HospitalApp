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
  SharedModule,
  PaginationModule,
  ModalModule
} from '@coreui/angular';
import { ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from './pagination/pagination.component';
import { AddUserModalComponent } from './add-user-modal/add-user-modal.component';
import { UpdateUserViewComponent } from './update-user-view/update-user-view.component';

@NgModule({
  declarations: [TextInputComponent, PaginationComponent, AddUserModalComponent, UpdateUserViewComponent],
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
    CommonModule,
    PaginationModule,
    PaginationComponent,
    AddUserModalComponent,
    UpdateUserViewComponent,
    ModalModule
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
    CommonModule,
    PaginationModule,
    ModalModule,
  ]
})
export class CommonSharedModule { }
