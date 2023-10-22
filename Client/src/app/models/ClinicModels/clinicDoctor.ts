import { ClinicAppointment } from "./clinicAppointments";

export interface ClinicDoctor {
    id:               number;
    fullName:       string;
    appointments?: ClinicAppointment[];
}