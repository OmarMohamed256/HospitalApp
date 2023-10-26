import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryService } from 'src/app/core/services/inventory.service';
import { SellOrderService } from 'src/app/core/services/sell-order.service';
import { InventoryItem } from 'src/app/models/InventoryModels/inventoryItems';
import { SellOrder } from 'src/app/models/InventoryModels/sellOrder';
import { InventoryItemParams } from 'src/app/models/Params/inventoryItemParams';

@Component({
  selector: 'app-sell-order-modal',
  templateUrl: './sell-order-modal.component.html',
  styleUrls: ['./sell-order-modal.component.scss']
})
export class SellOrderModalComponent implements OnInit{
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() sellOrderAddedUpdated = new EventEmitter<SellOrder>();
  inventoryItemParams: InventoryItemParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  inventoryItems: Partial<InventoryItem>[] = [];
  sellOrderForm!: FormGroup;

  constructor(private fb: FormBuilder, private sellOrderService: SellOrderService,
    private invetoryService: InventoryService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }
  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  searchItems(event: any) {
    if (event.term.trim().length > 1) {
      this.inventoryItemParams.searchTerm = event.term;
      this.invetoryService.getInventoryItems(this.inventoryItemParams).subscribe(response => {
        this.inventoryItems = response.result;
      });
    }
  }
  intializeForm() {
    this.sellOrderForm = this.fb.group({
      id: [0, Validators.required],
      quantity: [0, Validators.required],
      sellPrice: [0, Validators.required],
      note: [''],
      inventoryItemId: [0, Validators.required],
      soldTo: [''],
      includeExpiredItems: [false, Validators.required],
    });
  }

  createUpdateSellOrder() {
    if(this.sellOrderForm.value.id == 0) {
      this.createSellOrder();      
    }else {
      this.updateSellOrder();
    }
    this.modelToggeled(false);
  }

  createSellOrder() {
    this.sellOrderService.createSellOrder(this.sellOrderForm.value).subscribe(response => {
      this.sellOrderAddedUpdated.emit(response);
    })
  }
  updateSellOrder() {
    this.sellOrderService.updateSellOrder(this.sellOrderForm.value).subscribe(response => {
      this.sellOrderAddedUpdated.emit(response);
    })
  }
}
