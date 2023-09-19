using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager,
         ITokenService tokenService, IMapper mapper, RoleManager<AppRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
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

            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString()) ?? throw new ApiException(404, "User Not Found");

            user.FullName = userUpdateDto.FullName ?? user.FullName;
            user.Age = userUpdateDto.Age ?? user.Age;
            user.Gender = userUpdateDto.Gender ?? user.Gender;
            user.PhoneNumber = userUpdateDto.PhoneNumber ?? user.PhoneNumber;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }



    }
}