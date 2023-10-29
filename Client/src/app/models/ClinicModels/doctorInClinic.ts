import { Appointment } from "../appointment";

export interface DoctorInClinic {
    id:                         number;
    fullName:                   string;
    bookedWithAppointments?:    Appointment[]; 
}