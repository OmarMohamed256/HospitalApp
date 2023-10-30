import { Component, OnInit, ViewChild } from '@angular/core';
import { InventoryService } from 'src/app/core/services/inventory.service';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { InventoryItemParams } from 'src/app/models/Params/inventoryItemParams';
import { InventoryItem } from 'src/app/models/InventoryModels/inventoryItems';
import { Pagination } from 'src/app/models/pagination';
import { Speciality } from 'src/app/models/speciality';
import { InventoryItemModalComponent } from './inventory-item-modal/inventory-item-modal.component';

@Component({
  selector: 'app-dashboard-inventory',
  templateUrl: './dashboard-inventory.component.html',
  styleUrls: ['./dashboard-inventory.component.scss']
})
export class DashboardInventoryComponent implements OnInit {
  inventoryItems: InventoryItem[] | null = [];
  inventoryItemParams: InventoryItemParams = {
    pageNumber: 1,
    pageSize: 15,
    searchTerm: '',
    specialityId: null
  };
  pagination: Pagination | null = null;
  specialityList: Speciality[] = [];
  activePane = 0;
  modalVisibility: boolean = false;
  @ViewChild(InventoryItemModalComponent) IInventoryModal!: InventoryItemModalComponent;

  constructor(private inventoryService: InventoryService, private specialityService: SpecialityService) {
  }
  openModal() {
    this.modalVisibility = !this.modalVisibility
  }
  ngOnInit(): void {
    this.getInventoryItems();
    this.getSpecialities();
  }

  getInventoryItems() {
    this.inventoryService.getInventoryItems(this.inventoryItemParams).subscribe(response => {
      this.inventoryItems = response.result;
      this.pagination = response.pagination
    })
  }
  getSpecialities() {
    this.specialityService.getSpecialities().subscribe(response => {
      this.specialityList = response;
    })
  }
  getSpecialityNameById(id: number): string {
    const speciality = this.specialityList.find(item => item.id === id);
    return speciality ? speciality.name : '';
  }
  pageChanged(event: number) {
    this.inventoryItemParams.pageNumber = event;
    this.getInventoryItems();
  }

  onTabChange($event: number) {
    this.activePane = $event;
  }

  resetFilters() {
    this.inventoryItemParams = this.inventoryService.resetParams();
  }

  IItemAddedUpdated(inventoryItem: InventoryItem) {
    this.getInventoryItems();
  }

  setIItemAndShowModal(inventoryItem: InventoryItem) {
    this.mapIItemToForm(inventoryItem);
    this.openModal();
  }

  mapIItemToForm(inventoryItem: InventoryItem) {
    this.IInventoryModal.createUpdateInventoryItemForm.get("id")?.setValue(inventoryItem.id);
    this.IInventoryModal.createUpdateInventoryItemForm.get("name")?.setValue(inventoryItem.name);
    this.IInventoryModal.createUpdateInventoryItemForm.get("inventoryItemSpecialityId")?.setValue(inventoryItem.inventoryItemSpecialityId);
    this.IInventoryModal.createUpdateInventoryItemForm.get('name')?.disable();
    this.IInventoryModal.createUpdateInventoryItemForm.get('inventoryItemSpecialityId')?.disable();
  }

  initFormAndToggleModel() {
    this.IInventoryModal.intializeForm();
    this.openModal();
  }
}
