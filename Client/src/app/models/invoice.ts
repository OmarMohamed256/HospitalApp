// Generated by https://quicktype.io

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
    dateCreated:           string;
    dateUpdated:           string;
    appointmentId:         number;
    customItems:           CustomItem[];
    invoiceDoctorServices: InvoiceDoctorService[];
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