import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { InventoryItemParams } from 'src/app/models/inventoryItemParams';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { InventoryItem } from 'src/app/models/inventoryItems';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  baseUrl = environment.apiUrl;
  inventoryItemParams: InventoryItemParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  itemsCache = new Map();

  constructor(private http: HttpClient) { }

  resetParams() {
    this.inventoryItemParams = new InventoryItemParams();
    return this.inventoryItemParams;
  }

  getInventoryItems(inventoryItemParams: InventoryItemParams) {
    var response = this.itemsCache.get(Object.values(inventoryItemParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(inventoryItemParams.pageNumber, inventoryItemParams.pageSize);

    if (inventoryItemParams.searchTerm.trim() !== '') params = params.append('searchTerm', inventoryItemParams.searchTerm.trim());
    if (inventoryItemParams.specialityId !== null) params = params.append('specialityId', inventoryItemParams.specialityId);
    
    return getPaginatedResult<InventoryItem[]>(this.baseUrl + 'inventoryItem/', params, this.http)
      .pipe(map(response => {
        this.itemsCache.set(Object.values(inventoryItemParams).join("-"), response);
        return response;
      }))
  }
}
