import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { OrderParams } from 'src/app/models/Params/OrderParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { SupplyOrder } from 'src/app/models/InventoryModels/supplyOrder';

@Injectable({
  providedIn: 'root'
})
export class SupplyOrderService {
  baseUrl = environment.apiUrl;
  supplyOrderParams: OrderParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    inventoryItemId: null,
    orderBy: 'dateCreated',
    order: 'desc'
  };

  supplyOrderCache = new Map();

  constructor(private http: HttpClient) { }

  getSupplyOrders(supplyOrderParams: OrderParams) {
    var response = this.supplyOrderCache.get(Object.values(supplyOrderParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(supplyOrderParams.pageNumber, supplyOrderParams.pageSize);
    params = params.append('orderBy', supplyOrderParams.orderBy);
    params = params.append('order', supplyOrderParams.order);
    if (supplyOrderParams.searchTerm.trim() !== '') params = params.append('searchTerm', supplyOrderParams.searchTerm.trim());
    if (supplyOrderParams.inventoryItemId !== null) params = params.append('inventoryItemId', supplyOrderParams.inventoryItemId);
    return getPaginatedResult<SupplyOrder[]>(this.baseUrl + 'supplyOrder/', params, this.http)
      .pipe(map(response => {
        this.supplyOrderCache.set(Object.values(supplyOrderParams).join("-"), response);
        return response;
      }));
  }

  createSupplyOrder(supplyOrder: SupplyOrder) {
    return this.http.post<SupplyOrder>(this.baseUrl + 'supplyOrder/', supplyOrder).pipe(
      map(response => {
        this.invalidateSupplyOrderCache();
        return response;
      })
    );
  }
  
  updateSupplyOrder(supplyOrder: SupplyOrder) {
    return this.http.put<SupplyOrder>(this.baseUrl + 'supplyOrder/', supplyOrder).pipe(
      map(response => {
        this.invalidateSupplyOrderCache();
        return response;
      })
    );
  }

  private invalidateSupplyOrderCache() {
    this.supplyOrderCache.clear();
  }
  
  resetParams() {
    this.supplyOrderParams = new OrderParams();
    return this.supplyOrderParams;
  }
}
