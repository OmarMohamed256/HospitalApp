import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { PaginatedSupplyOrderState } from './supply-order.reducer';

export const selectPaginatedSupplyOrders = (state: AppState) => state.paginatedSupplyOrders;

export const SelectSupplyOrders = createSelector(
    selectPaginatedSupplyOrders,
    (state: PaginatedSupplyOrderState) => state.supplyOrders
);

export const SelectPagination = createSelector(
    selectPaginatedSupplyOrders,
    (state: PaginatedSupplyOrderState) => state.pagination
);

export const SelectSupplyOrdersWithPagination = createSelector(
    SelectSupplyOrders,
    SelectPagination,
    (supplyOrders, pagination) => ({ supplyOrders, pagination })
);
