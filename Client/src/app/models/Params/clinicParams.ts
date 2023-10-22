import { PaginationParams } from "./paginationParams";

export class ClinicParams extends PaginationParams {
    includeUpcomingAppointments = false;
    appointmentDateOfVisit? = '';
    clinicSpecialityId = null;
}