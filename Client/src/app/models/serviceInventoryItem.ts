import { InventoryItem } from "./inventoryItems";
import { Service } from "./service";

export interface ServiceInventoryItem {
    id?:              number;
    serviceId?:       number;
    inventoryItemId?: number;
    quantityNeeded?:  number;
    inventoryItem?:   InventoryItem;
    service?: Service;
}
