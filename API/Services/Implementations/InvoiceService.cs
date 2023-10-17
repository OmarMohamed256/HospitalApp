using System.Transactions;
using API.Errors;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.SignalR;
using AutoMapper;
using HospitalApp.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace API.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        private readonly ISupplyOrderRepository _supplyOrderRepository;
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<AppointmentHub, INotificationHub> _appointmentNotification;

        public InvoiceService(IInvoiceRepository invoiceRepository,
            IDoctorServiceRepository doctorServiceRepository, ISupplyOrderRepository supplyOrderRepository,
            IMapper mapper, IAppoinmentRepository appoinmentRepository, IHubContext<AppointmentHub, INotificationHub> appointmentNotification)
        {
            _invoiceRepository = invoiceRepository;
            _doctorServiceRepository = doctorServiceRepository;
            _supplyOrderRepository = supplyOrderRepository;
            _appoinmentRepository = appoinmentRepository;
            _mapper = mapper;
            _appointmentNotification = appointmentNotification;
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
                        invoice.InvoiceDoctorService.Add(await GetInvoiceDoctorService(invoice, invoiceDoctorServiceDto));
                    }
                    // Update invoice total
                    UpdateInvoiceTotalsAsync(invoice);
                    _invoiceRepository.AddInvoice(invoice);
                    if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed adding invoice");

                    await FinalizeAppointment(invoiceDto.AppointmentId, invoice.Id);
                    scope.Complete();
                    return _mapper.Map<InvoiceDto>(invoice);
                }
                else
                {
                    throw new BadRequestException("Services Not Supplied");
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to add invoice");
            }
        }
        private async Task<Invoice> AddInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var (type, priceVisitTemp, priceRevisitTemp) =
                await _appoinmentRepository.GetAppointmentTypeAndDoctorPricesAsync(invoiceDto.AppointmentId);
            if (type == null) throw new Exception("Invalid AppointmentId or missing type");

            decimal appointmentTypePrice;
            if (type == "visit") appointmentTypePrice = priceVisitTemp ?? 0;
            else if (type == "revisit") appointmentTypePrice = priceRevisitTemp ?? 0;
            else throw new Exception("Invalid appointment type");

            var invoice = new Invoice
            {
                AppointmentId = invoiceDto.AppointmentId,
                DiscountPercentage = invoiceDto.DiscountPercentage,
                PaymentMethod = invoiceDto.PaymentMethod,
                TotalPaid = invoiceDto.TotalPaid,
                TotalDue = appointmentTypePrice,
                FinalizationDate = DateTime.Now,
                AppointmentTypePrice = appointmentTypePrice,
                InvoiceDoctorService = new List<InvoiceDoctorService>(),
                // Add custom items
                CustomItems = new List<CustomItem>()
            };

            if (invoiceDto.CustomItems != null && invoiceDto.CustomItems.Any())
            {
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
            return invoice;
        }

        private async Task<InvoiceDoctorService> GetInvoiceDoctorService(Invoice invoice,
        CreateInvoiceDoctorServiceDto invoiceDoctorServiceDto)
        {
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
            invoiceDoctorService.InvoiceDoctorServiceSupplyOrders =
                await GetInvoiceDoctorServiceSupplyOrders(doctorService, invoiceDoctorService);
            return invoiceDoctorService;
        }

        private async Task<ICollection<InvoiceDoctorServiceSupplyOrders>> GetInvoiceDoctorServiceSupplyOrders(DoctorService doctorService,
        InvoiceDoctorService invoiceDoctorService)
        {
            ICollection<InvoiceDoctorServiceSupplyOrders> invoiceDoctorServiceSupplyOrders =
                new List<InvoiceDoctorServiceSupplyOrders>();
            if (doctorService.Service.ServiceInventoryItems == null) return invoiceDoctorServiceSupplyOrders;
            foreach (var item in doctorService.Service.ServiceInventoryItems)
            {
                // retrieve supply orders
                var consumableSupplyOrders = await _supplyOrderRepository
                    .GetConsumableSupplyOrdersByInventoryItemId(item.InventoryItemId);
                int quantityNeeded = item.QuantityNeeded * invoiceDoctorService.ServiceQuantity;
                int totalQuantity = consumableSupplyOrders.Sum(order => order.Quantity);
                if (totalQuantity < quantityNeeded)
                    throw new BadRequestException("Not enough supply orders to fulfill quantity needed for an item in: " + doctorService.Service.Name + " Service");
                foreach (var supplyOrder in consumableSupplyOrders)
                {
                    if (quantityNeeded > 0)
                    {
                        int quantityToConsume = Math.Min(quantityNeeded, supplyOrder.Quantity);
                        supplyOrder.Quantity -= quantityToConsume;
                        quantityNeeded -= quantityToConsume;
                        var invoiceDoctorServiceSupplyOrder = new InvoiceDoctorServiceSupplyOrders
                        {
                            InvoiceDoctorServiceId = invoiceDoctorService.Id,
                            ItemPrice = supplyOrder.ItemPrice,
                            QuantityUsed = quantityToConsume,
                            SupplyOrderId = supplyOrder.Id,
                            TotalPrice = supplyOrder.ItemPrice * quantityToConsume
                        };
                        invoiceDoctorServiceSupplyOrders.Add(invoiceDoctorServiceSupplyOrder);
                        _supplyOrderRepository.UpdateSupplyOrder(supplyOrder);
                        invoiceDoctorService.TotalDisposablesPrice += invoiceDoctorServiceSupplyOrder.TotalPrice;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            invoiceDoctorService.TotalPrice += invoiceDoctorService.TotalDisposablesPrice;
            return invoiceDoctorServiceSupplyOrders;
        }
        private void UpdateInvoiceTotalsAsync(Invoice invoice)
        {
            // Calculate total price including custom items
            decimal totalPriceWithCustomItems = invoice.CustomItems?.Sum(item => item.TotalPrice) ?? 0;
            invoice.CustomItemsTotalPrice = totalPriceWithCustomItems;
            invoice.TotalDue += totalPriceWithCustomItems;
            // Calculate total price including InvoiceDoctorService
            decimal totalPriceWithInvoiceDoctorService = invoice.InvoiceDoctorService?.Sum(item => item.TotalPrice) ?? 0;
            invoice.TotalDue += totalPriceWithInvoiceDoctorService;
            // update totals with discount here
            invoice.TotalAfterDiscount = invoice.TotalDue * (1 - (invoice.DiscountPercentage / 100));
            invoice.TotalRemaining = invoice.TotalAfterDiscount - invoice.TotalPaid;
        }
        private async Task FinalizeAppointment(int appointmentId, int invoiceId)
        {
            var updatedRecords = await _appoinmentRepository.UpdateAppointmentInvoicedAsync(appointmentId, "finalized", invoiceId);
            if (updatedRecords <= 0) throw new Exception("Failed to update Appointment status");
            await SendAppointmentFinalized(appointmentId);
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId) ?? throw new Exception("invoice does not exist");
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task SendAppointmentFinalized(int appointmentId)
        {
            await _appointmentNotification.Clients.All.SendAppointmentFinalized(new AppointmentStatus { AppointmentId = appointmentId });
        }
    }
}