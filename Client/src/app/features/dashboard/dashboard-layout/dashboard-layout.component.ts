import { Component } from '@angular/core';
import { navItems } from './_nav';
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from 'src/app/icons/icon-subset';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: ['./dashboard-layout.component.scss']
})
export class DashboardLayoutComponent {
  public navItems = navItems;
  constructor(private iconSetService: IconSetService)
  {
    iconSetService.icons = { ...iconSubset };
  }
}
