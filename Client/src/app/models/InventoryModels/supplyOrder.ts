export interface SupplyOrder {
    id:              number;
    quantity:        number;
    itemPrice:       number;
    note:            string;
    itemName?:       string;
    inventoryItemId: number;
    expiryDate:      Date;
    supplierName?:         string;
    sellPrice:         number;
}
