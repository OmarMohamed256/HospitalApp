import { Injectable } from '@angular/core';
import { of, map } from 'rxjs';
import { OrderParams } from 'src/app/models/Params/OrderParams';
import { environment } from 'src/environments/environment.development';
import { getPaginationHeaders, getPaginatedResult } from './paginationHelper';
import { SellOrder } from 'src/app/models/InventoryModels/sellOrder';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SellOrderService {
  baseUrl = environment.apiUrl;
  sellOrderParams: OrderParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    inventoryItemId: null,
    orderBy: 'dateCreated',
    order: 'desc'
  };

  sellOrderCache = new Map();

  constructor(private http: HttpClient) { }

  getSellOrders(sellOrderParams: OrderParams) {
    var response = this.sellOrderCache.get(Object.values(sellOrderParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(sellOrderParams.pageNumber, sellOrderParams.pageSize);
    params = params.append('orderBy', sellOrderParams.orderBy);
    params = params.append('order', sellOrderParams.order);
    if (sellOrderParams.searchTerm.trim() !== '') params = params.append('searchTerm', sellOrderParams.searchTerm.trim());
    if (sellOrderParams.inventoryItemId !== null) params = params.append('inventoryItemId', sellOrderParams.inventoryItemId);
    return getPaginatedResult<SellOrder[]>(this.baseUrl + 'sellOrder/', params, this.http)
      .pipe(map(response => {
        this.sellOrderCache.set(Object.values(sellOrderParams).join("-"), response);
        return response;
      }));
  }
  createSellOrder(sellOrder: SellOrder) {
    return this.http.post<SellOrder>(this.baseUrl + 'sellOrder/', sellOrder).pipe(
      map(response => {
        this.invalidateSellOrderCache();
        return response;
      })
    );
  }
  
  updateSellOrder(sellOrder: SellOrder) {
    return this.http.put<SellOrder>(this.baseUrl + 'sellOrder/', sellOrder).pipe(
      map(response => {
        this.invalidateSellOrderCache();
        return response;
      })
    );
  }

  invalidateSellOrderCache() {
    this.sellOrderCache.clear();
  }
  
  resetParams() {
    this.sellOrderParams = new OrderParams();
    return this.sellOrderParams;
  }
}
