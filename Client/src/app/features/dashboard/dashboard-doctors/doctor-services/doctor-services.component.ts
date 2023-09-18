import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DoctorServiceService } from 'src/app/core/services/doctor-service.service';
import { DoctorService } from 'src/app/models/doctorService';

@Component({
  selector: 'app-doctor-services',
  templateUrl: './doctor-services.component.html',
  styleUrls: ['./doctor-services.component.scss']
})
export class DoctorServicesComponent implements OnInit {
  doctor_services: DoctorService[] | null = [];
  @Input() doctorId: string = '';
  visible = false;
  updateDoctorServiceForm!: FormGroup;
  doctorServiceToUpdate!: DoctorService;
  validationErrors: string[] = [];

  constructor(
    private doctorServiceService: DoctorServiceService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getDoctorServicesById();
    this.initializeForm();
  }

  getDoctorServicesById() {
    this.doctorServiceService.getDoctorServiceByDocorId(this.doctorId).subscribe(response => {
      this.doctor_services = response;
    });
  }

  toggleLiveDemo() {
    this.visible = !this.visible;
  }

  openModal(service: Partial<DoctorService>) {
    this.visible = true;
    this.mapFormToDoctorService(service);
  }

  mapFormToDoctorService(doctorService: Partial<DoctorService>) {
    this.updateDoctorServiceForm.setValue({
      doctorPercentage: doctorService.doctorPercentage,
      hospitalPercentage: doctorService.hospitalPercentage,
      serviceName: doctorService.service?.name,
      id: doctorService.id,
    });
  }

  initializeForm() {
    this.updateDoctorServiceForm = this.fb.group({
      serviceName: [{ value: '', disabled: true }, Validators.required],
      id: ['', Validators.required],
      doctorPercentage: [0, [Validators.required, Validators.max(100)]],
      hospitalPercentage: [0, [Validators.required, Validators.max(100)]],
    });

    this.updateDoctorServiceForm.valueChanges.subscribe(formValue => {
      this.doctorServiceToUpdate = { ...this.doctorServiceToUpdate, ...formValue };
    });
  }

  updateDoctorServiceById() {
    this.doctorServiceService.updateDoctorServiceById(this.doctorServiceToUpdate).subscribe({
      next: (response) => {
        const index = this.doctor_services!.findIndex(ds => ds.id === this.doctorServiceToUpdate.id);

        if (index !== -1) {
          this.doctor_services![index] = {
            ...this.doctor_services![index],
            doctorPercentage: this.doctorServiceToUpdate.doctorPercentage,
            hospitalPercentage: this.doctorServiceToUpdate.hospitalPercentage
          };
        }
        this.toggleLiveDemo();
      },
      error: (error) => {
        this.validationErrors = error;
      }
    });
  }

  handleLiveDemoChange(event: any) {
    this.visible = event;
  }
}
