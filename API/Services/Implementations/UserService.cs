using API.Constants;
using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Models.DTOS.ImageDtos;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager, IMapper mapper, IImageRepository imageRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }


        public async Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams)
        {
            PagedList<AppUser> users = await _userRepository.GetAllUsersAsync(userParams);

            var userInfoDtos = _mapper.Map<IEnumerable<UserInfoDto>>(users);

            return new PagedList<UserInfoDto>(userInfoDtos, users.TotalCount, users.CurrentPage, users.PageSize);
        }

        public async Task<UserInfoDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserInfoDto>(user);
        }

        public async Task<bool> UpdateUser(UserUpdateDto userUpdateDto)
        {

            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString())
                ?? throw new ApiException(HttpStatusCode.NotFound, "User Not Found");

            user.FullName = userUpdateDto.FullName ?? user.FullName;
            user.Age = userUpdateDto.Age ?? user.Age;
            user.Gender = userUpdateDto.Gender ?? user.Gender;
            user.PhoneNumber = userUpdateDto.PhoneNumber ?? user.PhoneNumber;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PriceVisit = userUpdateDto.PriceVisit ?? user.PriceVisit;
            user.PriceRevisit = userUpdateDto.PriceRevisit ?? user.PriceRevisit;
            if (!string.IsNullOrEmpty(userUpdateDto.CurrentPassword) && !string.IsNullOrEmpty(userUpdateDto.NewPassword))
            {
                IdentityResult changePassResult =
                    await _userManager.ChangePasswordAsync(user, userUpdateDto.CurrentPassword, userUpdateDto.NewPassword);
                if (!changePassResult.Succeeded)
                {
                    var errorMessage = string.Join(", ", changePassResult.Errors.Select(e => e.Description));
                    throw new Exception("Password Change Failed: " + errorMessage);
                }
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<ImageDto> UploadImage(ImageUploadDto imageUploadDto)
        {
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageUploadDto.UserId + "_" + imageUploadDto.Category
                + "_" + imageUploadDto.File.FileName;

                string filePath = Path.Combine("images", uniqueFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageUploadDto.File.CopyToAsync(stream);
                Image image = new()
                {
                    Id = 0,
                    Url = uniqueFileName,
                    UserId = imageUploadDto.UserId,
                    Category = imageUploadDto.Category,
                    ImageDate = imageUploadDto.ImageDate,
                    DateCreated = DateTime.Now,
                    Organ = imageUploadDto.Organ,
                    Type = imageUploadDto.Type,
                };
                _imageRepository.AddImage(image);
                if (await _imageRepository.SaveAllAsync()) return _mapper.Map<ImageDto>(image);
                throw new Exception("Failed to add image to the database");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}