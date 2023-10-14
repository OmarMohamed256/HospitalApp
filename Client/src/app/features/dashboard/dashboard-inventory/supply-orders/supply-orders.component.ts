import { Component, OnInit } from '@angular/core';
import { SupplyOrderService } from 'src/app/core/services/supply-order.service';
import { SupplyOrderParams } from 'src/app/models/Params/supplyOrderParams';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app/core/state/app.state';
import { SupplyOrderActions } from 'src/app/core/state/supplyOrder/supply-order.actions';
import { SelectSupplyOrdersWithPagination } from 'src/app/core/state/supplyOrder/supply-order.selectors';

@Component({
  selector: 'app-supply-orders',
  templateUrl: './supply-orders.component.html',
  styleUrls: ['./supply-orders.component.scss']
})
export class SupplyOrdersComponent implements OnInit {
  supplyOrdersWithPagination$ = this.store.select(SelectSupplyOrdersWithPagination);
  supplyOrderParams: SupplyOrderParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    inventoryItemId: null,
    orderBy: 'dateCreated',
    order: 'desc'
  };
  orderByList = [{ value: 'dateOfExpiry', display: 'Expiry Date' },
  { value: 'dateCreated', display: 'Date Created' }, { value: 'dateUpdated', display: 'Date Updated' }];
  orderList = [{ value: 'asc', display: 'Ascending' }, { value: 'desc', display: 'Descending' }];
  modalVisibility: boolean = false;

  constructor(private store: Store<AppState>) {
  }
  ngOnInit(): void {
    this.getSupplyOrders();
  }

  getSupplyOrders() {
    const newParams = { ...this.supplyOrderParams };
    this.store.dispatch(SupplyOrderActions.loadSupplyOrders({ supplyOrderParams: newParams }));
  }

  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }

  resetFilters() {
    this.supplyOrderParams = {
      pageNumber: 1,
      pageSize: 15,
      searchTerm: '',
      inventoryItemId: null,
      orderBy: 'dateCreated',
      order: 'desc'
    };
    this.getSupplyOrders()
  }
  pageChanged(event: number) {
    this.supplyOrderParams.pageNumber = event;
    this.getSupplyOrders();
  }
}
