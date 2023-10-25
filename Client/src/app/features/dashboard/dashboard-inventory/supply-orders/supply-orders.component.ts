import { Component, OnInit, ViewChild } from '@angular/core';
import { SupplyOrderService } from 'src/app/core/services/supply-order.service';
import { Pagination } from 'src/app/models/pagination';
import { SupplyOrder } from 'src/app/models/InventoryModels/supplyOrder';
import { OrderParams } from 'src/app/models/Params/OrderParams';
import { SupplyOrderModelComponent } from './supply-order-model/supply-order-model.component';
import { InventoryItem } from 'src/app/models/InventoryModels/inventoryItems';

@Component({
  selector: 'app-supply-orders',
  templateUrl: './supply-orders.component.html',
  styleUrls: ['./supply-orders.component.scss']
})
export class SupplyOrdersComponent  implements OnInit{
  supplyOrders: SupplyOrder[] | null = [];
  supplyOrderParams: OrderParams = {
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
  @ViewChild(SupplyOrderModelComponent) supplyOrderModal!: SupplyOrderModelComponent;

  constructor(private supplyOrderService: SupplyOrderService) {
  }
  ngOnInit(): void {
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
  }
  pageChanged(event: number) {
    this.supplyOrderParams.pageNumber = event;
    this.getSupplyOrders();
  }

  supplyOrderAddedUpdated(supplyOrder: SupplyOrder) {
    this.resetFiltersAndGetSupplyOrders();
  }
  resetFiltersAndGetSupplyOrders() {
    this.resetFilters()
    this.getSupplyOrders()
  }
  setSupplyOrderAndShowModal(supplyOrder: SupplyOrder) {
    this.mapInventoryItemToList(supplyOrder);
    this.mapSupplyOrderTocreateUpdateSupplyOrderForm(supplyOrder);
    this.toggleModal();
  }

  initFormAndToggleModel() {
    this.supplyOrderModal.intializeForm();
    this.toggleModal();
  }

  mapInventoryItemToList(supplyOrder: SupplyOrder) {
    let inventoryItem: Partial<InventoryItem> = {
      id: supplyOrder.inventoryItemId,
      name: supplyOrder.itemName
    }
    this.supplyOrderModal.inventoryItems = [inventoryItem]
  }


  mapSupplyOrderTocreateUpdateSupplyOrderForm(supplyOrder: SupplyOrder) {
    this.supplyOrderModal.supplyOrderForm.get("id")?.setValue(supplyOrder.id);
    this.supplyOrderModal.supplyOrderForm.get("quantity")?.setValue(supplyOrder.quantity);
    this.supplyOrderModal.supplyOrderForm.get("itemPrice")?.setValue(supplyOrder.itemPrice);
    this.supplyOrderModal.supplyOrderForm.get("sellPrice")?.setValue(supplyOrder.sellPrice);
    this.supplyOrderModal.supplyOrderForm.get("note")?.setValue(supplyOrder.note);
    this.supplyOrderModal.supplyOrderForm.get("inventoryItemId")?.setValue(supplyOrder.inventoryItemId);
    this.supplyOrderModal.supplyOrderForm.get('inventoryItemId')?.disable();
    // Convert ISO 8601 date string to Date object
    const expiryDate = new Date(supplyOrder.expiryDate);

    // Get the date portion
    const formattedDate = expiryDate.toISOString().split('T')[0];
    this.supplyOrderModal.supplyOrderForm.get("expiryDate")?.setValue(formattedDate);
  }

}
