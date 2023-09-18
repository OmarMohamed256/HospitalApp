import { Component, Input, OnInit } from '@angular/core';
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
  constructor(private doctorServiceService: DoctorServiceService) {
  }
  ngOnInit(): void {
    this.getDoctorServicesById();
  }
  getDoctorServicesById() {
    this.doctorServiceService.getDoctorServiceByDocorId(this.doctorId).subscribe(response => {
      this.doctor_services = response;
    })
  }
}
