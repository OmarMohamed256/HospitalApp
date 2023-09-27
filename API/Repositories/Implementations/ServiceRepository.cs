using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddService(Service service)
        {
            _context.Services.Add(service);
        }

        public void UpdateService(Service service)
        {
            _context.Services.Update(service);
        }

        public void DeleteService(Service service)
        {
            _context.Services.Remove(service);
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _context.Services
                .Include(s => s.ServiceInventoryItems)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Service>> GetServicesAsync(ServiceParams serviceParams)
        {
            var query = _context.Services.AsQueryable();
            if (serviceParams.SpecialityId != null)
                query = query.Where(u => u.ServiceSpecialityId == serviceParams.SpecialityId);
            if (!string.IsNullOrEmpty(serviceParams.SearchTerm)) query = query.Where(u => u.Name.Contains(serviceParams.SearchTerm));


            return await PagedList<Service>.CreateAsync(query, serviceParams.PageNumber, serviceParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<int>> GetServicesIdsBySpecialityId(int specialityId)
        {
            return await _context.Services
                .Where(s => s.ServiceSpecialityId == specialityId)
                .Select(d => d.Id)
                .ToListAsync();
        }

        public async Task AddServiceInventoryItemsRangeAsync(ICollection<ServiceInventoryItem> ServiceInventoryItems)
        {
            await _context.ServiceInventoryItems.AddRangeAsync(ServiceInventoryItems);
        }

        public async Task<ICollection<ServiceInventoryItem>> GetServiceInventoryItemsByServiceId(int serviceId)
        {
            return await _context.ServiceInventoryItems.Where(sit => sit.ServiceId == serviceId).ToListAsync();
        }

        public void DeleteServiceInventoryItemsRangeAsync(ICollection<ServiceInventoryItem> ServiceInventoryItems)
        {
            _context.ServiceInventoryItems.RemoveRange(ServiceInventoryItems);
        }
    }
}