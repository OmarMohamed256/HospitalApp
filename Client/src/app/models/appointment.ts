import { Speciality } from "./speciality";
import { UserData } from "./UserModels/userData";

export interface Appointment {
    id?:                      number;
    status:                  string;
    type:                    string;
    dateCreated?:             Date;
    dateUpdated?:             Date;
    dateOfVisit:             Date;
    creationNote?:            string;
    doctorId:                number;
    patientId:               number;
    appointmentSpecialityId: number;
    doctor?:                  UserData;
    patient?:                  UserData;
    speciality?:              Speciality;
}


