import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryService } from 'src/app/core/services/inventory.service';
import { InventoryItem } from 'src/app/models/InventoryModels/inventoryItems';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-inventory-item-modal',
  templateUrl: './inventory-item-modal.component.html',
  styleUrls: ['./inventory-item-modal.component.scss']
})
export class InventoryItemModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() inventoryItemUpdatedCreated = new EventEmitter<InventoryItem>();
  createUpdateInventoryItemForm!: FormGroup;

  @Input() specialityList: Speciality[] = [];

  constructor(private fb: FormBuilder, private inventoryService: InventoryService) {
  }

  ngOnInit(): void {
    this.intializeForm();
  }

  createUpdateSpeciality() {
    if (this.createUpdateInventoryItemForm.get('id')?.value == 0) {
      this.createInventoryItem();
    } else {
      this.updateInventoryItem();
    }
    this.modelToggeled(false);

  }

  updateInventoryItem() {
    this.inventoryService.updateInventoryItem(this.createUpdateInventoryItemForm.value).subscribe(response => {
      this.inventoryItemUpdatedCreated.emit(response);
    })
  }

  createInventoryItem() {
    this.inventoryService.createInventoryItem(this.createUpdateInventoryItemForm.value).subscribe(response => {
      this.inventoryItemUpdatedCreated.emit(response);
    })
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createUpdateInventoryItemForm = this.fb.group({
      id: [0, Validators.required],
      name: ['', Validators.required],
      inventoryItemSpecialityId: ['', Validators.required],
    });
  }

}
