import { Component, OnInit, ViewChild } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { ToastrService } from 'ngx-toastr';
import { ServiceService } from 'src/app/core/services/service.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { iconSubset } from 'src/app/icons/icon-subset';
import { Pagination } from 'src/app/models/pagination';
import { Service } from 'src/app/models/service';
import { ServiceParams } from 'src/app/models/serviceParams';
import { Speciality } from 'src/app/models/speciality';
import { AddServiceModalComponent } from './add-service-modal/add-service-modal.component';

@Component({
  selector: 'app-dashboard-services',
  templateUrl: './dashboard-services.component.html',
  styleUrls: ['./dashboard-services.component.scss']
})
export class DashboardServicesComponent implements OnInit {
  services: Service[] | null = [];
  serviceParams: ServiceParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  pagination: Pagination | null = null;
  specialityList: Speciality[] = [];
  modalVisibility: boolean = false;
  @ViewChild(AddServiceModalComponent) addServiceModal!: AddServiceModalComponent;

  constructor(private iconSetService: IconSetService,
    private serviceService: ServiceService, private specialityService: SpecialityService, private toastr: ToastrService)
  {
    iconSetService.icons = { ...iconSubset };
  }

  ngOnInit(): void {
    this.getServices();
    this.getSpecialities();
  }

  getServices() {
    this.serviceService.getServices(this.serviceParams).subscribe(response => {
      this.services = response.result;
      this.pagination = response.pagination
    })
  }

  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
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

  deleteService(serviceId: number, event: Event) {
    event.stopPropagation();
    this.serviceService.deleteService(serviceId).subscribe({
      next: (response) => {
        this.services = this.services?.filter(service => service.id !== serviceId)!;
        this.toastr.success("Service deleted successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }

  openModal() {
    this.modalVisibility = !this.modalVisibility
  }

  setServiceToUpdate(service: Service) {
    this.addServiceModal.service = service;
    this.addServiceModal.intializeForm();
    this.openModal();
  }

  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }

  onServiceCreated(newService: Service) {
    if (newService) {
      const index = this.services!.findIndex(service => service.id === newService.id);
  
      if (index !== -1) {
        // Service with the same ID already exists, replace it
        this.services![index] = newService;
      } else {
        // Service with this ID doesn't exist, add it
        this.services!.push(newService);
      }
    }
  }
  
  

}
