import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DoctorServiceService } from 'src/app/core/services/doctor-service.service';
import { Appointment } from 'src/app/models/appointment';
import { DoctorService } from 'src/app/models/doctorService';
import { Service } from 'src/app/models/service';

@Component({
  selector: 'app-finalize-appointment',
  templateUrl: './finalize-appointment.component.html',
  styleUrls: ['./finalize-appointment.component.scss']
})
export class FinalizeAppointmentComponent implements OnInit{
  appointment?: Appointment;
  doctor_services: DoctorService[] | null = [];
  services: Service[] | null = [];

  createInvoiceForm!: FormGroup;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private doctorServiceService: DoctorServiceService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.appointment = data['appointment'];
      this.getDoctorServicesByDoctorId();
    })
  }

  getDoctorServicesByDoctorId() {
    this.doctorServiceService.getDoctorServiceByDocorId(this.appointment!.doctorId.toString()).subscribe(response => {
      this.doctor_services = response;
      this.services = this.doctor_services.map(item => item.service);
      console.log(this.services)
    });
  }

  initializeForm() {
    this.createInvoiceForm = this.fb.group({
      selectedServices: this.fb.array([])
    });
  }
}
