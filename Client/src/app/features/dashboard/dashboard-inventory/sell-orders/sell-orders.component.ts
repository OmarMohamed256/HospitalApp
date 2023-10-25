import { Component, OnInit } from '@angular/core';
import { SellOrderService } from 'src/app/core/services/sell-order.service';
import { SellOrder } from 'src/app/models/InventoryModels/sellOrder';
import { OrderParams } from 'src/app/models/Params/OrderParams';
import { Pagination } from 'src/app/models/pagination';

@Component({
  selector: 'app-sell-orders',
  templateUrl: './sell-orders.component.html',
  styleUrls: ['./sell-orders.component.scss']
})
export class SellOrdersComponent implements OnInit{
  sellOrders: SellOrder[] | null = [];
  sellOrderParams: OrderParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    inventoryItemId: null,
    orderBy: 'dateCreated',
    order: 'desc'
  };
  pagination: Pagination | null = null;
  orderByList = [{ value: 'dateOfExpiry', display: 'Expiry Date' },
  { value: 'dateCreated', display: 'Date Created' }, { value: 'dateUpdated', display: 'Date Updated' }];
  orderList = [{ value: 'asc', display: 'Ascending' }, { value: 'desc', display: 'Descending' }];

  constructor(private sellOrderService: SellOrderService) {
  }
  ngOnInit(): void {
    this.getSellOrders();
  }
  getSellOrders() {
    this.sellOrderService.getSellOrders(this.sellOrderParams).subscribe(response => {
      this.sellOrders = response.result;
      this.pagination = response.pagination
    })
  }
  resetFilters() {
    this.sellOrderParams = this.sellOrderService.resetParams();
  }
  pageChanged(event: number) {
    this.sellOrderParams.pageNumber = event;
    this.getSellOrders();
  }
  resetFiltersAndGetSellOrders() {
    this.resetFilters()
    this.getSellOrders()
  }
}
