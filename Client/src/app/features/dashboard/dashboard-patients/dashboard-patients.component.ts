import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from 'src/app/icons/icon-subset';

@Component({
  selector: 'app-dashboard-patients',
  templateUrl: './dashboard-patients.component.html',
  styleUrls: ['./dashboard-patients.component.scss']
})
export class DashboardPatientsComponent implements OnInit{
  constructor(private iconSetService: IconSetService)
  {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
  }
    
}
