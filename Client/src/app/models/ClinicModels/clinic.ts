import { ClinicDoctor } from "./clinicDoctor";export interface Clinic {
    id:               number;
    clinicNumber:       string;
    clinicDoctors?: ClinicDoctor[];
}
