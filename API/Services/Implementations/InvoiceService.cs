using System.Transactions;
using API.Errors;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        private readonly ISupplyOrderRepository _supplyOrderRepository;
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;
        public InvoiceService(IInvoiceRepository invoiceRepository,
            IDoctorServiceRepository doctorServiceRepository, ISupplyOrderRepository supplyOrderRepository,
            IMapper mapper, IAppoinmentRepository appoinmentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _doctorServiceRepository = doctorServiceRepository;
            _supplyOrderRepository = supplyOrderRepository;
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
        }
        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // 1- Add invoice
                var invoice = await AddInvoiceAsync(invoiceDto);

                // 2- Create list of InvoiceDoctorService
                if (invoiceDto.InvoiceDoctorServices != null)
                {
                    foreach (var invoiceDoctorServiceDto in invoiceDto.InvoiceDoctorServices)
                    {
                        await AddDoctorServiceToInvoiceAsync(invoiceDto, invoice, invoiceDoctorServiceDto);
                    }

                    // Update invoice total
                    await UpdateInvoiceTotalsAsync(invoice);

                    FinalizeAppointment(invoiceDto.AppointmentId);

                    scope.Complete();
                    return _mapper.Map<InvoiceDto>(invoice);
                }
                else
                {
                    throw new Exception("Services Not Supplied");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<Invoice> AddInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                AppointmentId = invoiceDto.AppointmentId,
                DiscountPercentage = invoiceDto.DiscountPercentage,
                PaymentMethod = invoiceDto.PaymentMethod,
                TotalPaid = invoiceDto.TotalPaid,
                TotalDue = 0,
            };
            // Add custom items
            if (invoiceDto.CustomItems != null)
            {
                invoice.CustomItems = new List<CustomItem>();
                foreach (var customItemDto in invoiceDto.CustomItems)
                {
                    var customItem = new CustomItem
                    {
                        Name = customItemDto.Name,
                        Price = customItemDto.Price,
                        Units = customItemDto.Units,
                        TotalPrice = customItemDto.Price * customItemDto.Units
                    };
                    invoice.CustomItems.Add(customItem);
                }
            }
            _invoiceRepository.AddInvoice(invoice);
            if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed adding invoice");
            return invoice;
        }

        private async Task AddDoctorServiceToInvoiceAsync(CreateInvoiceDto invoiceDto, Invoice invoice,
        CreateInvoiceDoctorServiceDto invoiceDoctorServiceDto)
        {
            // retrieve service
            var doctorServiceId = invoiceDoctorServiceDto.DoctorServiceId;
            var doctorService = await _doctorServiceRepository.GetDoctorServiceWithServiceAndItemsById(doctorServiceId);
            var invoiceDoctorService = new InvoiceDoctorService
            {
                DoctorServiceId = invoiceDoctorServiceDto.DoctorServiceId,
                ServiceQuantity = invoiceDoctorServiceDto.ServiceQuantity,
                ServiceName = doctorService.Service.Name,
                InvoiceId = invoice.Id,
                ServiceSoldPrice = doctorService.Service.TotalPrice,
                TotalDisposablesPrice = 0,
                TotalPrice = doctorService.Service.TotalPrice * invoiceDoctorServiceDto.ServiceQuantity
            };
            _invoiceRepository.AddInvoiceDoctorService(invoiceDoctorService);
            if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed to add invoice doctor service");

            // Call the function to handle inventory items
            await HandleInventoryItemsAsync(doctorService, invoiceDoctorService, invoiceDoctorServiceDto, invoice);
        }

        private async Task HandleInventoryItemsAsync(DoctorService doctorService,
        InvoiceDoctorService invoiceDoctorService, CreateInvoiceDoctorServiceDto invoiceDoctorServiceDto, Invoice invoice)
        {
            if (doctorService.Service.ServiceInventoryItems == null) return;
            // retrieve items
            foreach (var item in doctorService.Service.ServiceInventoryItems)
            {
                // retrieve supply orders
                var consumableSupplyOrders = await _supplyOrderRepository.GetConsumableSupplyOrdersByInventoryItemId(item.InventoryItemId);
                int quantityNeeded = item.QuantityNeeded * invoiceDoctorService.ServiceQuantity;
                int totalQuantity = consumableSupplyOrders.Sum(order => order.Quantity);
                if (totalQuantity < quantityNeeded) throw new Exception("Not enough supply orders to fulfill quantity needed for an item in: " + doctorService.Service.Name + " Service");
                
                foreach (var supplyOrder in consumableSupplyOrders)
                {
                    if (quantityNeeded > 0)
                    {
                        int quantityToConsume = Math.Min(quantityNeeded, supplyOrder.Quantity); // Determine how much can be consumed from this supply order
                        supplyOrder.Quantity -= quantityToConsume;
                        quantityNeeded -= quantityToConsume;
                        // update supply order and add a new InvoiceDoctorServiceSupplyOrders
                        var invoiceDoctorServiceSupplyOrder = new InvoiceDoctorServiceSupplyOrders
                        {
                            InvoiceDoctorServiceId = invoiceDoctorService.Id,
                            ItemPrice = supplyOrder.ItemPrice,
                            QuantityUsed = quantityToConsume,
                            SupplyOrderId = supplyOrder.Id,
                            TotalPrice = supplyOrder.ItemPrice * quantityToConsume
                        };
                        _invoiceRepository.AddInvoiceDoctorServiceOrders(invoiceDoctorServiceSupplyOrder);
                        _supplyOrderRepository.UpdateSupplyOrder(supplyOrder);
                        if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed to update/add supply orders");
                        invoiceDoctorService.TotalDisposablesPrice += invoiceDoctorServiceSupplyOrder.TotalPrice;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            invoiceDoctorService.TotalPrice += invoiceDoctorService.TotalDisposablesPrice;
            invoice.TotalDue += invoiceDoctorService.TotalPrice;
        }

        private async Task UpdateInvoiceTotalsAsync(Invoice invoice)
        {
            // Calculate total price including custom items
            decimal totalPriceWithCustomItems = invoice.CustomItems?.Sum(item => item.TotalPrice) ?? 0;
            invoice.CustomItemsTotalPrice = totalPriceWithCustomItems;
            invoice.TotalDue += totalPriceWithCustomItems;

            // update totals with discount here
            invoice.TotalAfterDiscount = invoice.TotalDue * (1 - (invoice.DiscountPercentage / 100));
            invoice.TotalRemaining = invoice.TotalAfterDiscount - invoice.TotalPaid;

            var saveInvoiceResult = await _invoiceRepository.SaveAllAsync();
            if (!saveInvoiceResult) throw new Exception("Failed to save Invoice");
        }
        private void FinalizeAppointment(int appointmentId)
        {
            var updatedRecords = _appoinmentRepository.UpdateAppointmentStatus(appointmentId, "finalized");
            if (updatedRecords <= 0) throw new Exception("Failed to update Appointment status");
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId) ?? throw new Exception("invoice does not exist");
            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}