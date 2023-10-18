using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> SaveAllAsync();
    }
}