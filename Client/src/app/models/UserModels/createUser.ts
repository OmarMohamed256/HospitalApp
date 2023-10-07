import { DoctorWorkingHours } from "./doctorWorkingHours";

export interface CreateUser {
    username:              string;
    email:                 string;
    gender:                string;
    age:                   number;
    fullName:              string;
    phoneNumber:           string;
    password:              string;
    doctorSpecialityId:    number;
    doctorWorkingHours: DoctorWorkingHours[];
    role:                  string;
    dateCreated:           string;
    dateUpdated:           string;
}

