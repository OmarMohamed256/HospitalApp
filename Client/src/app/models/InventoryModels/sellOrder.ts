export interface SellOrder {
    id:                     number;
    quantity:               number;
    sellPrice:              number;
    note:                   string;
    itemName?:              string;
    inventoryItemId:        number;
    soldTo?:                string;
    includeExpiredItems:    boolean;
}