import { inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { Appointment } from "src/app/models/appointment";
import { AppointmentService } from "../services/appointment.service";

export const AppointmentDetailedResolver: ResolveFn<Appointment> = (route, state) => {
    const appointmentService = inject(AppointmentService);
    return appointmentService.getAppointmentById(route.paramMap.get('id')!);
};