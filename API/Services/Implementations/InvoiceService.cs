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
                var invoice = await InitializeInvoiceAsync(invoiceDto);
                // 2- Create invoice custom items 
                if (invoiceDto.CustomItems != null && invoiceDto.CustomItems.Any())
                    invoice.CustomItems = PopulateCustomItems(invoiceDto.CustomItems);
                // 3- Create list of InvoiceDoctorService
                invoice.InvoiceDoctorService = new List<InvoiceDoctorService>();
                if (invoiceDto.InvoiceDoctorServices != null)
                {
                    foreach (var invoiceDoctorServiceDto in invoiceDto.InvoiceDoctorServices)
                        invoice.InvoiceDoctorService.Add(await GetInvoiceDoctorService(invoice, invoiceDoctorServiceDto));
                }
                // 4- Update invoice total
                UpdateInvoiceTotalsAsync(invoice);
                // 5- Add invoice
                _invoiceRepository.AddInvoice(invoice);
                if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed adding invoice");
                // 6- Finalize appointment
                await FinalizeAppointment(invoiceDto.AppointmentId, invoice.Id);
                scope.Complete();
                return _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add invoice");
            }
        }
        public async Task<InvoiceDto> UpdateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {

                var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceDto.Id) ?? throw new BadRequestException("Invoice Not Found");
                decimal appointmentTypePrice = await GetAppointmentTypePrice(invoice.AppointmentId);
                // map certain data
                invoice.DiscountPercentage = invoiceDto.DiscountPercentage;
                invoice.PaymentMethod = invoiceDto.PaymentMethod;
                invoice.TotalPaid = invoiceDto.TotalPaid;
                invoice.TotalDue = appointmentTypePrice;
                invoice.FinalizationDate = DateTime.Now;
                // 2- Create invoice custom items 
                if (invoiceDto.CustomItems != null && invoiceDto.CustomItems.Any())
                    invoice.CustomItems = PopulateCustomItems(invoiceDto.CustomItems);
                // 3- Create list of InvoiceDoctorService
                invoice.InvoiceDoctorService = new List<InvoiceDoctorService>();
                if (invoiceDto.InvoiceDoctorServices != null)
                {
                    foreach (var invoiceDoctorServiceDto in invoiceDto.InvoiceDoctorServices)
                        invoice.InvoiceDoctorService.Add(await GetInvoiceDoctorService(invoice, invoiceDoctorServiceDto));
                }
                // 4- Update invoice total
                UpdateInvoiceTotalsAsync(invoice);
                // 5- Update invoice
                _invoiceRepository.UpdateInvoice(invoice);

                if (!await _invoiceRepository.SaveAllAsync()) throw new ApiException(500, "Failed adding invoice");
                // 6- Finalize appointment
                await FinalizeAppointment(invoiceDto.AppointmentId, invoice.Id);
                scope.Complete();
                return _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add invoice");
            }
        }

        private async Task<decimal> GetAppointmentTypePrice(int appointmentId)
        {
            var (type, priceVisitTemp, priceRevisitTemp) =
                await _appoinmentRepository.GetAppointmentTypeAndDoctorPricesAsync(appointmentId);
            if (type == null) throw new Exception("Invalid AppointmentId or missing type");

            decimal appointmentTypePrice;
            if (type == "visit") appointmentTypePrice = priceVisitTemp ?? 0;
            else if (type == "revisit") appointmentTypePrice = priceRevisitTemp ?? 0;
            else throw new Exception("Invalid appointment type");
            return appointmentTypePrice;
        }
        private async Task<Invoice> InitializeInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            decimal appointmentTypePrice = await GetAppointmentTypePrice(invoiceDto.AppointmentId);
            var invoice = new Invoice
            {
                Id = invoiceDto.Id,
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
            return invoice;
        }
        private static ICollection<CustomItem> PopulateCustomItems(ICollection<CreateCustomItemDto> customItemsDto)
        {
            List<CustomItem> customItems = new();
            foreach (var customItemDto in customItemsDto)
            {
                var customItem = new CustomItem
                {
                    Name = customItemDto.Name,
                    Price = customItemDto.Price,
                    Units = customItemDto.Units,
                    TotalPrice = customItemDto.Price * customItemDto.Units
                };
                customItems.Add(customItem);
            }
            return customItems;
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
        private static void UpdateInvoiceTotalsAsync(Invoice invoice)
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