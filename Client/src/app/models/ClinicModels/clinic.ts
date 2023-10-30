import { Appointment } from "../appointment";
import { ClinicDoctor } from "./clinicDoctor";export interface Clinic {
    id:               number;
    clinicNumber:       string;
    clinicDoctors?: ClinicDoctor[];
    upcomingAppointments?:  Appointment[];
}
