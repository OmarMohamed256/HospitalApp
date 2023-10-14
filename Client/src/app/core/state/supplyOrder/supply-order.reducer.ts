import { createReducer, on } from '@ngrx/store';
import { SupplyOrder } from 'src/app/models/supplyOrder';
import { SupplyOrderActions } from './supply-order.actions';
import { PaginatedResult, Pagination } from 'src/app/models/pagination';
import { SupplyOrderParams } from 'src/app/models/Params/supplyOrderParams';

export interface PaginatedSupplyOrderState {
    supplyOrders: SupplyOrder[];
    pagination: Pagination | null,
    error: string | null;
    status: 'pending' | 'loading' | 'error' | 'success';
    supplyOrderParams: SupplyOrderParams | null;
    cache: { [cacheKey: string]: PaginatedResult<SupplyOrder[]> };
}

export const initialState: PaginatedSupplyOrderState = {
    supplyOrders: [],
    pagination: null,
    error: null,
    status: 'pending',
    supplyOrderParams: null,
    cache: {}
};


export const PaginatedSupplyOrderReducer = createReducer(
    initialState,
    on(SupplyOrderActions.loadSupplyOrders, (state, {supplyOrderParams}) =>
     ({ ...state, supplyOrderParams: supplyOrderParams ,status: 'loading' as const })),

     on(SupplyOrderActions.loadSupplyOrdersSuccess, (state, { paginatedResult }) => {
        if (!state.supplyOrderParams) {
            return state;
        }
        const cacheKey = generateCacheKey(state.supplyOrderParams);
        
        return {
            ...state,
            supplyOrders: paginatedResult.result || [],
            status: 'success' as const,
            pagination: paginatedResult.pagination,
            cache: {
                ...state.cache,
                [cacheKey]: paginatedResult
            }
        };
    }),

    on(SupplyOrderActions.loadSupplyOrdersFailure, (state, { error }) => ({ ...state, error, status: 'error' as const })),

    on(SupplyOrderActions.createSupplyOrder, state => ({ ...state, status: 'loading' as const })),
    on(SupplyOrderActions.createSupplyOrderSuccess, (state, { supplyOrder }) =>
        ({ ...state, supplyOrders: [...state.supplyOrders, supplyOrder], status: 'success' as const })),
    on(SupplyOrderActions.createSupplyOrderFailure, (state, { error }) => ({ ...state, error: error, status: 'error' as const })),

    on(SupplyOrderActions.updateSupplyOrder, state => ({ ...state, status: 'loading' as const })),
    on(SupplyOrderActions.updateSupplyOrderSuccess, (state, { supplyOrder }) => {
        const updatedSupplyOrders = state.supplyOrders.map(order => order.id === supplyOrder.id ? supplyOrder : order);
        return { ...state, supplyOrders: updatedSupplyOrders, status: 'success' as const };
    }),
    on(SupplyOrderActions.updateSupplyOrderFailure, (state, { error }) => ({ ...state, error: error, status: 'error' as const })),

);

function generateCacheKey(supplyOrderParams: SupplyOrderParams): string {
    return Object.values(supplyOrderParams).join("-");
}