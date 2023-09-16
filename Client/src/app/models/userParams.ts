import { PaginationParams } from "./paginationParams";

export class UserParams extends PaginationParams {
  orderBy = 'date';
  order = 'asc';
  gender = '';
  searchTerm = '';
}
