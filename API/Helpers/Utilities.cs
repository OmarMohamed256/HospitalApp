using API.Models.DTOS;
using API.Models.Entities;

namespace API.Helpers
{
    public static class Utilities
    {
        public class InvoiceDoctorServiceComparer : IEqualityComparer<object>
        {
            public new bool Equals(object? x, object? y)
            {
                if (x == null || y == null)
                    return false;

                var dto = x as CreateInvoiceDoctorServiceDto;
                var service = y as InvoiceDoctorService;

                if (dto != null && service != null)
                    return dto.DoctorServiceId == service.DoctorServiceId && dto.ServiceQuantity == service.ServiceQuantity;
                else
                    return false;
            }

            public int GetHashCode(object obj)
            {
                if (obj == null)
                    return 0;

                var dto = obj as CreateInvoiceDoctorServiceDto;
                var service = obj as InvoiceDoctorService;

                if (dto != null)
                    return (dto.DoctorServiceId, dto.ServiceQuantity).GetHashCode();
                else if (service != null)
                    return (service.DoctorServiceId, service.ServiceQuantity).GetHashCode();
                else
                    return 0;
            }
        }

    }
}