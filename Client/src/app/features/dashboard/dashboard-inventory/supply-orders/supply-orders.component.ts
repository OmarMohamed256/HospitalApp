import { Component } from '@angular/core';
import { SupplyOrderService } from 'src/app/core/services/supply-order.service';
import { Pagination } from 'src/app/models/pagination';
import { SupplyOrder } from 'src/app/models/supplyOrder';
import { SupplyOrderParams } from 'src/app/models/Params/supplyOrderParams';

@Component({
  selector: 'app-supply-orders',
  templateUrl: './supply-orders.component.html',
  styleUrls: ['./supply-orders.component.scss']
})
export class SupplyOrdersComponent {
  supplyOrders: SupplyOrder[] | null = [];
  supplyOrderParams: SupplyOrderParams = {
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
  modalVisibility: boolean = false;

  constructor(private supplyOrderService: SupplyOrderService) {
    this.getSupplyOrders();
  }

  getSupplyOrders() {
    this.supplyOrderService.getSupplyOrders(this.supplyOrderParams).subscribe(response => {
      this.supplyOrders = response.result;
      this.pagination = response.pagination
    })
  }

  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }
  resetFilters() {
    this.supplyOrderParams = this.supplyOrderService.resetParams();
    this.getSupplyOrders()
  }
  pageChanged(event: number) {
    this.supplyOrderParams.pageNumber = event;
    this.getSupplyOrders();
  }

}
