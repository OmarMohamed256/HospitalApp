import { Role } from "./role";

export interface UserData {
    id: string;
    username:    string;
    email:       string;
    fullName:    string;
    phoneNumber: string;
    gender: string;
    age: string;
    dateCreated: string;
    dateUpdated: string;
    doctorSpecialityId?: number;
    userRoles?: Role[];
}
