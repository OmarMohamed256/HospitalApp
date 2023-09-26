import { Speciality } from "./speciality";
import { UserData } from "./userData";

export interface Appointment {
    id?:                      number;
    status:                  string;
    type:                    string;
    dateCreated?:             Date;
    dateUpdated?:             Date;
    dateOfVisit:             Date;
    doctorId:                number;
    patientId:               number;
    appointmentSpecialityId: number;
    doctor?:                  UserData;
    patient?:                  UserData;
    speciality?:              Speciality;
}


