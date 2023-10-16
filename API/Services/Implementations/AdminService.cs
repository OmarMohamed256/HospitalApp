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
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        public AdminService(UserManager<AppUser> userManager, IMapper mapper, IAuthenticationService authenticationService,
        IAdminRepository adminRepository, IDoctorServiceRepository doctorServiceRepository, IServiceRepository serviceRepository)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _adminRepository = adminRepository;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _doctorServiceRepository = doctorServiceRepository;
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
            bool userExists = await _authenticationService.UserExists(createUserDto.Username);
            if (userExists) throw new BadRequestException("Username is already taken");
            var user = new AppUser
            {
                UserName = createUserDto.Username.ToLower(),
                Email = createUserDto.Email,
                Gender = createUserDto.Gender,
                Age = createUserDto.Age,
                FullName = createUserDto.FullName,
                PhoneNumber = createUserDto.PhoneNumber,
                DoctorSpecialityId = createUserDto.DoctorSpecialityId,
                PriceRevisit = createUserDto.PriceRevisit,
                PriceVisit = createUserDto.PriceVisit
            };
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // Create User
                await CreateUser(user, createUserDto.Password);
                // Add Roles
                await AddUserRoles(user, createUserDto.Role);
                if (createUserDto.Role == Roles.Doctor)
                {
                    // Add doctor Working Hours
                    if (createUserDto.DoctorWorkingHours != null)
                        await AddDoctorWorkingHours(user.Id, createUserDto.DoctorWorkingHours);
                    // Add Doctor service
                    if (user.DoctorSpecialityId.HasValue)
                    {
                        await AddDoctorservices(user);
                    }
                }
                scope.Complete(); // Mark the transaction as complete, to be committed
                return _mapper.Map<UserInfoDto>(user);
            }
            catch (Exception)
            {
                // No need to explicitly rollback, TransactionScope will handle it automatically
                throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add user"); // Re-throw the exception for further handling
            }
        }
        private async Task AddDoctorservices(AppUser user)
        {
            var servicesWithSpecialityId =
                await _serviceRepository.GetServicesIdsBySpecialityId(user.DoctorSpecialityId.Value);
            if (servicesWithSpecialityId.Any())
            {
                await _doctorServiceRepository.CreateDoctorServicesForDoctor(user.Id, servicesWithSpecialityId);
                bool saveDoctorService = await _serviceRepository.SaveAllAsync();
                if (!saveDoctorService) throw new Exception("Failed to add doctor services");
            }
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

        private async Task AddDoctorWorkingHours(int id, ICollection<DoctorWorkingHoursDto> doctorWorkingHours)
        {
            foreach (var workingHour in doctorWorkingHours)
            {
                workingHour.DoctorId = id;
                if (workingHour.StartTime >= workingHour.EndTime) 
                    throw new BadRequestException("End time must be greater than start time.");
            }
            // Add woking hours to doctor
            var workingHours = _mapper.Map<IEnumerable<DoctorWorkingHours>>(doctorWorkingHours);
            await _adminRepository.AddDoctorWorkingHours(workingHours);
            var adminResult = await _adminRepository.SaveAllAsync();
            if (!adminResult) throw new Exception("Failed to add working hours");
        }

        public async Task ToggleLockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            if (!user.LockoutEnabled)
            {
                // lock user
                var lockUserResult = await _userManager.SetLockoutEnabledAsync(user, true);
                if (!lockUserResult.Succeeded) throw new Exception("Failed to lock user");
                var lockDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddYears(200));
                if (!lockDateResult.Succeeded) throw new Exception("Failed to set lockout end date");
            }
            else
            {
                // unlock user
                var setLockoutEndDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.Subtract(TimeSpan.FromMinutes(1)));
                if (!setLockoutEndDateResult.Succeeded) throw new Exception("Failed to set lockout end date");
                var unLockUserResult = await _userManager.SetLockoutEnabledAsync(user, false);
                if (!unLockUserResult.Succeeded) throw new Exception("Failed to unlock user");
            }
            var invalidateToken = await _userManager.UpdateSecurityStampAsync(user);
            if (!invalidateToken.Succeeded) throw new Exception("failed to invalidate token");
        }
    }
}