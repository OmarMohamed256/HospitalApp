// Generated by https://quicktype.io

export interface SupplyOrder {
    id:              number;
    quantity:        number;
    itemPrice:       number;
    note:            string;
    itemName?:        string;
    inventoryItemId: number;
    expiryDate:      Date;
}
