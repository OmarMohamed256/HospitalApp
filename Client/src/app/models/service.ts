import { ServiceInventoryItem } from "./serviceInventoryItem";

export interface Service {
    id:                    number;
    name:                  string;
    totalPrice:            number;
    serviceSpecialityId:   number;
    serviceInventoryItems?: ServiceInventoryItem[];
}
