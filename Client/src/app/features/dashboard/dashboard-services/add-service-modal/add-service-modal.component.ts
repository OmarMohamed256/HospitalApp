import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { InventoryService } from 'src/app/core/services/inventory.service';
import { ServiceService } from 'src/app/core/services/service.service';
import { InventoryItemParams } from 'src/app/models/inventoryItemParams';
import { InventoryItem } from 'src/app/models/inventoryItems';
import { Service } from 'src/app/models/service';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-add-service-modal',
  templateUrl: './add-service-modal.component.html',
  styleUrls: ['./add-service-modal.component.scss']
})
export class AddServiceModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() serviceCreated = new EventEmitter<Service>();
  @Input() specialityList: Speciality[] = [];
  inventoryItems: InventoryItem[] = [];
  inventoryItemParams: InventoryItemParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };

  createServiceForm!: FormGroup;
  validationErrors: string[] = [];
  service: Service = {
    id: 0, // Assuming you don't have an ID until it's created
    name: '',
    totalPrice: 0,
    serviceSpecialityId: 0 // Assuming you don't have a speciality ID until it's selected
  };

  constructor(private fb: FormBuilder, private serviceService: ServiceService,
    private toastr: ToastrService, private invetoryService: InventoryService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }

  searchItems(event: any) {
    if (event.term.trim().length > 1) {
      this.inventoryItemParams.searchTerm = event.term;
      this.invetoryService.getInventoryItems(this.inventoryItemParams).subscribe(response => {
        this.inventoryItems = response.result;
      });
    }
  }

  onItemsSelect(items: InventoryItem[]) {
    console.log(items);
  }

  modelToggeled(e: any) {
    this.visible = e;
    if(!e) this.resetForm();
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createServiceForm = this.fb.group({
      id:[this.service.id, Validators.required],
      name: [this.service.name, Validators.required],
      totalPrice: [this.service.totalPrice, Validators.required],
      serviceSpecialityId: [this.service.serviceSpecialityId, Validators.required]
    });
  }

  createUpdateService() {
    this.mapFormToService();
    if (this.service.id == 0) {
      this.createService();
    }else {
      this.updateService();
    }
  }

  createService() {
    this.serviceService.createService(this.service).subscribe({
      next: (response) => {
        this.service = response as Service; // This should not overwrite the newService object
        this.serviceCreated.emit(this.service); // Emit the newService object
        this.resetForm();
        this.modelToggeled(false);
        this.toastr.success("Service created successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }

  updateService() {
    this.serviceService.updateService(this.service).subscribe({
      next: (response) => {
        this.service = response as Service; // This should not overwrite the newService object
        this.serviceCreated.emit(this.service); // Emit the newService object
        this.resetForm();
        this.modelToggeled(false);
        this.toastr.success("Service updated successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }


  resetForm() {
    this.intializeForm();
    this.service = {
      id: 0,
      name: '',
      totalPrice: 0,
      serviceSpecialityId: 0
    };
    this.createServiceForm.reset();
  }

  mapFormToService() {
    this.service = {
      id: this.createServiceForm.get('id')?.value,
      serviceSpecialityId: this.createServiceForm.get('serviceSpecialityId')?.value,
      name: this.createServiceForm.get('name')?.value,
      totalPrice: this.createServiceForm.get('totalPrice')?.value,
    }
  }
}
