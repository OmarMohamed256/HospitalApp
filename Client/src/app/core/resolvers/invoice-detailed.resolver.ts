import { ResolveFn } from "@angular/router";
import { Invoice } from "src/app/models/InvoiceModels/invoice";
import { InvoiceService } from "../services/invoice.service";
import { inject } from "@angular/core";
import { of } from 'rxjs';

export const InvoiceDetailedResolver: ResolveFn<Invoice> = (route, state) => {
    const invoiceService = inject(InvoiceService);
    if (route.queryParams.hasOwnProperty('invoice')) {
        const invoice = JSON.parse(route.queryParams['invoice']);
        if (invoice) {
            return of(invoice);
        }
    }
    return invoiceService.getInvoice(parseInt(route.paramMap.get('invoiceId')!));
};