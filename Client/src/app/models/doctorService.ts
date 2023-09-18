import { Service } from "./service";

export interface DoctorService {
    id:   number;
    doctorId:   number;
    serviceId:   number;
    doctorPercentage:   number;
    hospitalPercentage:   number;
    service: Service;
}