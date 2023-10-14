import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryService } from 'src/app/core/services/inventory.service';
import { SupplyOrderService } from 'src/app/core/services/supply-order.service';
import { InventoryItemParams } from 'src/app/models/Params/inventoryItemParams';
import { InventoryItem } from 'src/app/models/inventoryItems';
import { SupplyOrder } from 'src/app/models/supplyOrder';

@Component({
  selector: 'app-supply-order-model',
  templateUrl: './supply-order-model.component.html',
  styleUrls: ['./supply-order-model.component.scss']
})
export class SupplyOrderModelComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() supplyOrderAddedUpdated = new EventEmitter<SupplyOrder>();
  inventoryItemList: InventoryItem[] = [];
  inventoryItemParams: InventoryItemParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  inventoryItems: InventoryItem[] = [];
  supplyOrderForm!: FormGroup;


  constructor(private fb: FormBuilder, private supplyOrderService: SupplyOrderService,
    private invetoryService: InventoryService) {
  }

  ngOnInit(): void {
    this.intializeForm();
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
    this.supplyOrderForm = this.fb.group({
      id: [0, Validators.required],
      quantity: [0, Validators.required],
      itemPrice: [0, Validators.required],
      note: [''],
      inventoryItemId: [0, Validators.required],
      expiryDate: ['', Validators.required],
    });
  }

  createUpdateSupplyOrder() {
    if(this.supplyOrderForm.value.id == 0) {
      this.createSupplyOrder();      
    }else {
      this.updateSupplyOrder();
    }
  }

  createSupplyOrder() {
    this.supplyOrderService.createSupplyOrder(this.supplyOrderForm.value).subscribe(response => {
      this.supplyOrderAddedUpdated.emit(response);
    })
  }

  updateSupplyOrder() {
    this.supplyOrderService.updateSupplyOrder(this.supplyOrderForm.value).subscribe(response => {
      this.supplyOrderAddedUpdated.emit(response);
    })
  }

}
