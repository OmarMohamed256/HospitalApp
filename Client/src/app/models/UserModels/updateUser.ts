import { DoctorWorkingHours } from "./doctorWorkingHours";

export interface UpdateUser {
    id:                   string;
    username?:              string;
    email?:                 string;
    gender?:                string;
    age?:                   number;
    fullName?:              string;
    phoneNumber?:           string;
    doctorSpecialityId?:    number;
    doctorWorkingHours?: DoctorWorkingHours[];
    role?:                  string;
    priceVisit?:            number;
    priceRevisit?:          number;
}