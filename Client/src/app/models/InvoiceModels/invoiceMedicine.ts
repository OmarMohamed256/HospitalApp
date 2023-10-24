import { Medicine } from "../medicine";

export interface InvoiceMedicine {
    medicineId:      number;
    dosageAmount:    string
    duration:        string
    frequency:       string
    note?:           string
    invoiceId?:      number;
    medicine?:       Medicine;
}