import { CreateCustomItem } from "./createCustomItem";
import { CreateInvoiceDoctorService } from "./createInvoiceDoctorService";

export interface CreateInvoice {
    paymentMethod:         string;
    discountPercentage:    number;
    totalPaid:             number;
    appointmentId:         number;
    customItems:           CreateCustomItem[];
    invoiceDoctorServices: CreateInvoiceDoctorService[];
}


