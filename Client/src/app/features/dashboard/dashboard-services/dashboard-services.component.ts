import { Component, OnInit } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { ServiceService } from 'src/app/core/services/service.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { Pagination } from 'src/app/models/pagination';
import { Service } from 'src/app/models/service';
import { ServiceParams } from 'src/app/models/serviceParams';

@Component({
  selector: 'app-dashboard-services',
  templateUrl: './dashboard-services.component.html',
  styleUrls: ['./dashboard-services.component.scss']
})
export class DashboardServicesComponent implements OnInit{
  services: Service[] | null = [];
  serviceParams: ServiceParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  pagination: Pagination | null = null;

  constructor(private iconSetService: IconSetService, private serviceService: ServiceService)
  {
    iconSetService.icons = { ...iconSubset };
  }

  ngOnInit(): void {
    this.getServices();
  }

  getServices() {
    this.serviceService.getServices(this.serviceParams).subscribe(response => {
      this.services = response.result;
      this.pagination = response.pagination
      console.log(response)
    })
  }

  pageChanged(event: number) {
    this.serviceParams.pageNumber = event;
    this.getServices();
  }

  resetFilters() {
    this.serviceParams = this.serviceService.resetServiceParams();
    this.getServices()
  }
}
