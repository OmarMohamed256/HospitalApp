import { Medicine } from "../medicine";

export interface InvoiceMedicine {
    medicineId:     number;
    invoiceId?:      number;
    medicine?:       Medicine;
}