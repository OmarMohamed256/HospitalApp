import { ServiceInventoryItem } from "./InventoryModels/serviceInventoryItem";

export interface Service {
    id:                    number;
    name:                  string;
    totalPrice:            number;
    serviceSpecialityId:   number;
    serviceInventoryItems?: ServiceInventoryItem[];
}
