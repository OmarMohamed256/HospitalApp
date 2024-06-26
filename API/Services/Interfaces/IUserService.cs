using API.Helpers;
using API.Models.DTOS;
using API.Models.DTOS.ImageDtos;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams);
        Task<bool> UpdateUser(UserUpdateDto userUpdateDto);
        Task<UserInfoDto> GetUserById(string userId);
        Task<ImageDto> UploadImage(ImageUploadDto imageUploadDto);
        Task<ICollection<ImageDto>> GetAllUserImagesById(int userId);
    }
}