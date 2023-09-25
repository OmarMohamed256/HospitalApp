import { PaginationParams } from "./paginationParams";

export class SupplyOrderParams extends PaginationParams {
    orderBy = 'dateCreated';
    order = 'desc';
    inventoryItemId = null;
    searchTerm = '';
  }