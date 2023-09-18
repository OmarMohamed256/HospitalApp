import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ServiceService } from 'src/app/core/services/service.service';
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
  createServiceForm!: FormGroup;
  validationErrors: string[] = [];
  service: Service = {
    id: 0, // Assuming you don't have an ID until it's created
    name: '',
    disposablesPrice: 0,
    totalPrice: 0,
    serviceSpecialityId: 0 // Assuming you don't have a speciality ID until it's selected
  };

  constructor(private fb: FormBuilder, private serviceService: ServiceService,
    private toastr: ToastrService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createServiceForm = this.fb.group({
      name: [this.service.name, Validators.required],
      disposablesPrice: [this.service.disposablesPrice, Validators.required],
      totalPrice: [this.service.totalPrice, Validators.required],
      serviceSpecialityId: [this.service.serviceSpecialityId, Validators.required]
    });
    this.createServiceForm.valueChanges.subscribe(formValue => {
      this.service = { ...this.service, ...formValue };
    });
  }

  createUpdateService() {
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
        this.resetFormAndCloseModal();
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
        this.resetFormAndCloseModal();
        this.toastr.success("Service updated successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }
  // Add this method
  resetFormAndCloseModal() {
    this.intializeForm();
    this.service = { // Reset the service object
      id: 0,
      name: '',
      disposablesPrice: 0,
      totalPrice: 0,
      serviceSpecialityId: 0
    };
    this.createServiceForm.reset();
    this.modelToggeled(false); // Close the modal
  }
}
