import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ServiceService } from 'src/app/core/services/service.service';
import { Service } from 'src/app/models/service';

@Component({
  selector: 'app-add-service-modal',
  templateUrl: './add-service-modal.component.html',
  styleUrls: ['./add-service-modal.component.scss']
})
export class AddServiceModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() serviceCreated = new EventEmitter<Service>();
  @Input()   specialityList: { value: string, display: string }[] = [];
  createServiceForm!: FormGroup;
  validationErrors: string[] = [];
  service: Service = {
    id: 0, // Assuming you don't have an ID until it's created
    name: '',
    disposablesPercentage: 0,
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
      name: ['', Validators.required],
      disposablesPercentage: ['', Validators.required],
      totalPrice: ['', Validators.required],
      serviceSpecialityId: ['', Validators.required]
    })
  }

  createService() {
    this.mapFormToService();
    this.serviceService.createService(this.service).subscribe({
      next: (response) => {
        this.serviceCreated.emit(this.service);
        this.visible = false;
        this.toastr.success("Service created successfully")
      },
      error: (err) => {
        console.error(err);
        // Handle errors here
      }
    });
  }

  mapFormToService() {
    this.service.name = this.createServiceForm.value.name;
    this.service.disposablesPercentage = this.createServiceForm.value.disposablesPercentage;
    this.service.totalPrice = this.createServiceForm.value.totalPrice;
    this.service.serviceSpecialityId = this.createServiceForm.value.serviceSpecialityId;
  }
}
