import { PaginationParams } from "./paginationParams";

export class AppointmentParams extends PaginationParams {
    orderBy = 'date';
    order = 'asc';
    specialityId = null;
    type = '';
  }
  