import { CreateCustomItem } from "./createCustomItem";
import { CreateInvoiceDoctorService } from "./createInvoiceDoctorService";

export interface CreateInvoice {
    id?:                   number;
    paymentMethod:         string;
    discountPercentage:    number;
    totalPaid:             number;
    appointmentId:         number;
    customItems:           CreateCustomItem[];
    invoiceDoctorServices: CreateInvoiceDoctorService[];
}


