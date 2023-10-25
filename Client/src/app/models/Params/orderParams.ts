import { PaginationParams } from "./paginationParams";

export class OrderParams extends PaginationParams {
    orderBy = 'dateCreated';
    order = 'desc';
    inventoryItemId = null;
    searchTerm = '';
  }