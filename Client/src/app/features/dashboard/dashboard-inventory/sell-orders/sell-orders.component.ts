import { Component, OnInit, ViewChild } from '@angular/core';
import { SellOrderService } from 'src/app/core/services/sell-order.service';
import { SellOrder } from 'src/app/models/InventoryModels/sellOrder';
import { OrderParams } from 'src/app/models/Params/OrderParams';
import { Pagination } from 'src/app/models/pagination';
import { SellOrderModalComponent } from './sell-order-modal/sell-order-modal.component';
import { InventoryItem } from 'src/app/models/InventoryModels/inventoryItems';

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
  modalVisibility: boolean = false;
  @ViewChild(SellOrderModalComponent) sellOrderModal!: SellOrderModalComponent;

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
  sellOrderAddedUpdated(sellOrder: SellOrder) {
    this.getSellOrders()
  }
  initFormAndToggleModel() {
    this.sellOrderModal.intializeForm();
    this.toggleModal();
  }

  toggleModal() {
    this.modalVisibility = !this.modalVisibility
  }

  setSellOrderAndShowModal(sellOrder: SellOrder) {
    this.mapInventoryItemToList(sellOrder);
    this.mapSellOrderTocreateUpdateSellOrderForm(sellOrder);
    this.toggleModal();
  }

  mapInventoryItemToList(sellOrder: SellOrder) {
    let inventoryItem: Partial<InventoryItem> = {
      id: sellOrder.inventoryItemId,
      name: sellOrder.itemName
    }
    this.sellOrderModal.inventoryItems = [inventoryItem]
  }

  mapSellOrderTocreateUpdateSellOrderForm(sellOrder: SellOrder) {
    this.sellOrderModal.sellOrderForm.get("id")?.setValue(sellOrder.id);
    this.sellOrderModal.sellOrderForm.get("quantity")?.setValue(sellOrder.quantity);
    this.sellOrderModal.sellOrderForm.get("sellPrice")?.setValue(sellOrder.sellPrice);
    this.sellOrderModal.sellOrderForm.get("note")?.setValue(sellOrder.note);
    this.sellOrderModal.sellOrderForm.get("soldTo")?.setValue(sellOrder.soldTo);
    this.sellOrderModal.sellOrderForm.get("inventoryItemId")?.setValue(sellOrder.inventoryItemId);
    this.sellOrderModal.sellOrderForm.get("includeExpiredItems")?.setValue(sellOrder.includeExpiredItems);

    this.sellOrderModal.sellOrderForm.get('inventoryItemId')?.disable();
    this.sellOrderModal.sellOrderForm.get('quantity')?.disable();
    this.sellOrderModal.sellOrderForm.get('includeExpiredItems')?.disable();
  }
}
