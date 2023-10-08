import { RoomAppointment } from "./roomAppointments";

export interface RoomDoctor {
    id:               number;
    fullName:       string;
    appointments?: RoomAppointment[];
}