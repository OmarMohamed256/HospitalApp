import { ClinicDoctor } from "./clinicDoctor";

export interface Clinic {
    id:               number;
    clinicNumber:       string;
    clinicSpecialityId: number;
    doctorId:         number;
    doctor?:           ClinicDoctor;
}
