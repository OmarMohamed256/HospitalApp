import { DoctorInClinic } from "./doctorInClinic";

export interface ClinicDoctor {
    doctorId:       number;
    clinicId:       number;
    doctor?:        DoctorInClinic;
}