// Generated by https://quicktype.io

import { UserData } from "../UserModels/userData";
import { Appointment } from "../appointment";
import { InvoiceMedicine } from "./invoiceMedicine";

export interface Invoice {
    id:                    number;
    paymentMethod:         string;
    totalDue:              number;
    customItemsTotalPrice: number;
    discountPercentage:    number;
    totalAfterDiscount:    number;
    totalPaid:             number;
    totalRemaining:        number;
    finalizationDate:      string;
    appointmentId:         number;
    customItems:           CustomItem[];
    invoiceDoctorServices: InvoiceDoctorService[];
    appointment:           Appointment;
    appointmentTypePrice:  number;
    invoiceMedicines?:    InvoiceMedicine[];
    doctor:    UserData;
    patient:    UserData;
}

export interface CustomItem {
    id:         number;
    name:       string;
    price:      number;
    units:      number;
    totalPrice: number;
    invoiceId:  number;
}

export interface InvoiceDoctorService {
    id:                    number;
    invoiceId:             number;
    doctorServiceId:       number;
    serviceQuantity:       number;
    totalDisposablesPrice: number;
    totalPrice:            number;
    serviceSoldPrice:      number;
    serviceName:           string;
}
