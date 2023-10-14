import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { SupplyOrderActions } from './supply-order.actions';
import { catchError, map, of, switchMap, withLatestFrom } from 'rxjs';
import { SupplyOrderService } from '../../services/supply-order.service';
import { Store } from '@ngrx/store';
import { AppState } from '../app.state';
import { selectPaginatedSupplyOrders } from './supply-order.selectors';
import { SupplyOrderParams } from 'src/app/models/Params/supplyOrderParams';

@Injectable()
export class SupplyOrderEffects {
    loadSupplyOrders$ = createEffect(() =>
        this.actions$.pipe(
            ofType(SupplyOrderActions.loadSupplyOrders),
            withLatestFrom(this.store.select(selectPaginatedSupplyOrders)),
            switchMap(([{ supplyOrderParams }, state]) => {
                if (state.cache.hasOwnProperty(generateCacheKey(supplyOrderParams))) {
                    const cachedResult = state.cache[generateCacheKey(supplyOrderParams)];
                    return of(SupplyOrderActions.loadSupplyOrdersSuccess({ paginatedResult: cachedResult }));
                } else {
                    return this.supplyOrderService.getSupplyOrders(supplyOrderParams).pipe(
                        map(response => SupplyOrderActions.loadSupplyOrdersSuccess({ paginatedResult: response })),
                        catchError(error => of(SupplyOrderActions.loadSupplyOrdersFailure({ error })))
                    );
                }
            })
        )
    );



    constructor(
        private supplyOrderService: SupplyOrderService,
        private actions$: Actions,
        private store: Store<AppState>
    ) { }
}
function generateCacheKey(supplyOrderParams: SupplyOrderParams): string {
    return Object.values(supplyOrderParams).join("-");
}