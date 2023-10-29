import { ResolveFn } from "@angular/router";
import { Invoice } from "src/app/models/InvoiceModels/invoice";
import { InvoiceService } from "../services/invoice.service";
import { inject } from "@angular/core";

export const InvoiceDetailedResolver: ResolveFn<Invoice> = (route, state) => {
    const invoiceService = inject(InvoiceService);
    return invoiceService.getInvoice(parseInt(route.paramMap.get('invoiceId')!));
};