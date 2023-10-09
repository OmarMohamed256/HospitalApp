import { RoomDoctor } from "./roomDoctor";

export interface Room {
    id:               number;
    roomNumber:       string;
    roomSpecialityId: number;
    doctorId:         number;
    doctor?:           RoomDoctor;
}
