using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        void AddImage(Image image);
        void RemoveImage(Image image);
        Task<ICollection<Image>> GetImagesByUserIdAsync(int userId);
        Task<Image> GetImageByIdAsync(int imageId);
        Task<bool> SaveAllAsync();
    }
}