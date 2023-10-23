import { CreateCustomItem } from "./createCustomItem";
import { CreateInvoiceDoctorService } from "./createInvoiceDoctorService";
import { InvoiceMedicine } from "./invoiceMedicine";

export interface CreateInvoice {
    id?:                   number;
    paymentMethod:         string;
    discountPercentage:    number;
    totalPaid:             number;
    appointmentId:         number;
    customItems:           CreateCustomItem[];
    invoiceDoctorServices: CreateInvoiceDoctorService[];
    invoiceMedicines?:    InvoiceMedicine[];
}


