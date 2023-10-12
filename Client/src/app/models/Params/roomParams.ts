import { PaginationParams } from "./paginationParams";

export class RoomParams extends PaginationParams {
    includeUpcomingAppointments = false;
    appointmentDateOfVisit? = '';
    roomSpecialityId = null;
}