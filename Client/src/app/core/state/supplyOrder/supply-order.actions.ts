import { createAction, createActionGroup, emptyProps, props } from '@ngrx/store';
import { SupplyOrderParams } from 'src/app/models/Params/supplyOrderParams';
import { PaginatedResult, Pagination } from 'src/app/models/pagination';
import { SupplyOrder } from 'src/app/models/supplyOrder';

export const SupplyOrderActions = createActionGroup({
    source: '[Supply Order]',
    events: {
        loadSupplyOrders: props<{ supplyOrderParams: SupplyOrderParams }>(),
        loadSupplyOrdersSuccess: props<{ paginatedResult: PaginatedResult<SupplyOrder[]> }>(),
        loadSupplyOrdersFailure: props<{ error: any }>(),
        createSupplyOrder: props<{ supplyOrder: SupplyOrder }>(),
        createSupplyOrderSuccess: props<{ supplyOrder: SupplyOrder }>(),
        createSupplyOrderFailure: props<{ error: any }>(),
        updateSupplyOrder: props<{ supplyOrder: SupplyOrder }>(),
        updateSupplyOrderSuccess: props<{ supplyOrder: SupplyOrder }>(),
        updateSupplyOrderFailure: props<{ error: any }>()
    }
});
