import { Component } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { AccountService } from 'src/app/core/services/account.service';
import { IRoleNavData } from 'src/app/models/role-nav-data';
import { navItems } from './_nav';
import { iconSubset } from 'src/app/icons/icon-subset';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: ['./dashboard-layout.component.scss']
})
export class DashboardLayoutComponent {
  public navItems: IRoleNavData[] | undefined;

  constructor(
    private iconSetService: IconSetService,
    private accountService: AccountService
  ) {
    iconSetService.icons = { ...iconSubset };
  
    this.accountService.currentUser$.subscribe(user => {
      this.navItems = navItems.filter(item => this.checkUserRole(item, user.roles));
    });
  }

  private checkUserRole(item: IRoleNavData, userRoles: string[]): boolean {
    if (item.roles) {
      return item.roles.some(role => userRoles.includes(role));
    }
    return true;
  }
}