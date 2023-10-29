using System.Transactions;
using API.Constants;
using API.Errors;
using API.Models.DTOS;
using API.Models.DTOS.UserDtos;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Identity;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userReposiotry;
        public AdminService(UserManager<AppUser> userManager, IMapper mapper, IAuthenticationService authenticationService,
        IServiceRepository serviceRepository, IUserRepository userReposiotry)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _userReposiotry = userReposiotry;
        }

        public async Task ChangeUserRole(string userId, ChangeRoleDto changeRoleDto)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            var isInCurrentRole = await _userManager.IsInRoleAsync(user, changeRoleDto.CurrentRole);
            if (!isInCurrentRole) throw new Exception("User is not in " + changeRoleDto.CurrentRole + "role");
            await _userManager.RemoveFromRoleAsync(user, changeRoleDto.CurrentRole);
            await _userManager.AddToRoleAsync(user, changeRoleDto.NewRole);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to change user role to " + changeRoleDto.NewRole);
            var invalidateToken = await _userManager.UpdateSecurityStampAsync(user);
            if (!invalidateToken.Succeeded) throw new Exception("failed to invalidate token");
        }

        public async Task<UserInfoDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            await ValidateUserNameDoesntExist(createUserDto.Username);
            var user = new AppUser
            {
                Id = 0,
                UserName = createUserDto.Username.ToLower(),
                Email = createUserDto.Email,
                Gender = createUserDto.Gender,
                Age = createUserDto.Age,
                FullName = createUserDto.FullName,
                PhoneNumber = createUserDto.PhoneNumber,
                DoctorSpecialityId = createUserDto.DoctorSpecialityId,
                PriceRevisit = createUserDto.PriceRevisit,
                PriceVisit = createUserDto.PriceVisit,
                DoctorWorkingHours = _mapper.Map<ICollection<DoctorWorkingHours>>(createUserDto.DoctorWorkingHours)
            };
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                if (createUserDto.Role == Roles.Doctor)
                    if (user.DoctorSpecialityId.HasValue) user.DoctorServices = await PopulateDoctorDoctorServices(user);
                // Create User
                await CreateUser(user, createUserDto.Password);
                // Add Roles
                await AddUserRoles(user, createUserDto.Role);
                scope.Complete(); // Mark the transaction as complete, to be committed
                return _mapper.Map<UserInfoDto>(user);
            }
            catch (Exception)
            {
                // No need to explicitly rollback, TransactionScope will handle it automatically
                throw new Exception("Failed to add user"); // Re-throw the exception for further handling
            }
        }
        public async Task<UserInfoDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userReposiotry.GetUserWithDoctorServicesAndDoctorWorkingHoursByIdAsync(updateUserDto.Id)
                ?? throw new ApiException(HttpStatusCode.NotFound, "User Not Found");
            user.UserName = updateUserDto.Username.ToLower() ?? user.UserName;
            user.Email = updateUserDto.Email ?? user.Email;
            user.Gender = updateUserDto.Gender ?? user.Gender;
            user.Age = updateUserDto.Age;
            user.FullName = updateUserDto.FullName ?? user.FullName;
            user.PhoneNumber = updateUserDto.PhoneNumber ?? user.PhoneNumber;
            user.DoctorSpecialityId = updateUserDto.DoctorSpecialityId ?? user.DoctorSpecialityId;
            user.PriceRevisit = updateUserDto.PriceRevisit ?? user.PriceRevisit;
            user.PriceVisit = updateUserDto.PriceVisit ?? user.PriceVisit;
            user.DoctorWorkingHours = _mapper.Map<ICollection<DoctorWorkingHours>>(updateUserDto.DoctorWorkingHours) ?? user.DoctorWorkingHours;
            if (updateUserDto.Role == Roles.Doctor)
                if (user.DoctorSpecialityId.HasValue) user.DoctorServices = await PopulateDoctorDoctorServices(user);
            // Create User
            await UpdateUser(user);
            return _mapper.Map<UserInfoDto>(user);
            throw new Exception("Failed to add user"); // Re-throw the exception for further handling
        }
        private async Task UpdateUser(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to update user");
        }
        private async Task ValidateUserNameDoesntExist(string username)
        {
            bool userExists = await _authenticationService.UserExists(username);
            if (userExists) throw new BadRequestException("Username is already taken");
        }
        private async Task<ICollection<DoctorService>> PopulateDoctorDoctorServices(AppUser user)
        {
            ICollection<DoctorService> doctorServices = new List<DoctorService>();
            if (user.DoctorSpecialityId == null) return doctorServices;
            else
            {
                var serviceIdsWithSpecialityId = await GetServicesIdsListBySpecialityId(user.DoctorSpecialityId.Value);
                if (!serviceIdsWithSpecialityId.Any()) return doctorServices;
                doctorServices = serviceIdsWithSpecialityId.Select(serviceId => new DoctorService
                {
                    DoctorId = user.Id,
                    ServiceId = serviceId,
                    DoctorPercentage = 50,
                    HospitalPercentage = 50
                }).ToList();
                return doctorServices;
            }
        }
        private async Task<List<int>> GetServicesIdsListBySpecialityId(int specilaityId)
        {
            return await _serviceRepository.GetServicesIdsBySpecialityId(specilaityId);
        }
        private async Task CreateUser(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) throw new Exception("Failed to create user");
        }
        private async Task AddUserRoles(AppUser user, string role)
        {
            if (!Roles.IsValidRole(role)) throw new NotFoundException("Invalid role specified");
            var roleResults = await _userManager.AddToRoleAsync(user, role);
            if (!roleResults.Succeeded) throw new Exception("Failed to add role");
        }
        public async Task ToggleLockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            if (!user.LockoutEnabled)
            {
                // lock user
                var lockUserResult = await _userManager.SetLockoutEnabledAsync(user, true);
                if (!lockUserResult.Succeeded) throw new Exception("Failed to lock user");
                var lockDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(200));
                if (!lockDateResult.Succeeded) throw new Exception("Failed to set lockout end date");
            }
            else
            {
                // unlock user
                var setLockoutEndDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.Subtract(TimeSpan.FromMinutes(1)));
                if (!setLockoutEndDateResult.Succeeded) throw new Exception("Failed to set lockout end date");
                var unLockUserResult = await _userManager.SetLockoutEnabledAsync(user, false);
                if (!unLockUserResult.Succeeded) throw new Exception("Failed to unlock user");
            }
            var invalidateToken = await _userManager.UpdateSecurityStampAsync(user);
            if (!invalidateToken.Succeeded) throw new Exception("failed to invalidate token");
        }
    }
}