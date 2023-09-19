import { PaginationParams } from "./paginationParams";

export class UserParams extends PaginationParams {
  orderBy = 'date';
  order = 'asc';
  gender = '';
  searchTerm = '';
  roleName = '';
  
  doctorSpecialityId: number | null = null;

  constructor(init?:Partial<UserParams>) {
    super();
    Object.assign(this, init);
  }

}
