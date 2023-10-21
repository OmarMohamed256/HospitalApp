using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddImage(Image image)
        {
            _context.Images.Add(image);
        }
        public void RemoveImage(Image image)
        {
            _context.Images.Remove(image);
        }
        public async Task<Image> GetImageByIdAsync(int imageId)
        {
            return await _context.Images
            .Where(i => i.Id == imageId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Image>> GetImagesByUserIdAsync(int userId)
        {
           return await _context.Images.Where(i =>i.UserId == userId).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}