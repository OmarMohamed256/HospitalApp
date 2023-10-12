using System.Security.Claims;
using System.Transactions;
using API.Constants;
using API.Errors;
using API.Models.DTOS;
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

        public async Task<UserInfoDto> CreateUser(CreateUserDto createUserDto)
        {
            bool userExists = await _authenticationService.UserExists(createUserDto.Username);
            if (userExists) throw new ApiException(HttpStatusCode.BadRequest, "Username is already taken");
            var user = new AppUser
            {
                UserName = createUserDto.Username.ToLower(),
                Email = createUserDto.Email,
                Gender = createUserDto.Gender,
                Age = createUserDto.Age,
                FullName = createUserDto.FullName,
                PhoneNumber = createUserDto.PhoneNumber,
                DoctorSpecialityId = createUserDto.DoctorSpecialityId,
            };
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // Create User
                var result = await _userManager.CreateAsync(user, createUserDto.Password);
                if (!result.Succeeded) throw new ApiException(HttpStatusCode.InternalServerError, "Failed to create user");

                // Add Roles
                if (!Roles.IsValidRole(createUserDto.Role)) throw new ApiException(HttpStatusCode.NotFound, "Invalid role specified");
                var roleResults = await _userManager.AddToRoleAsync(user, createUserDto.Role);
                if (!roleResults.Succeeded) throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add role");
                // Add Claims
                await _userManager.AddClaimAsync(user, new Claim("IsUserDisabled", "false"));

                if (createUserDto.Role == Roles.Doctor)
                {
                    if (createUserDto.DoctorWorkingHours != null)
                    {
                        foreach (var workingHour in createUserDto.DoctorWorkingHours)
                        {
                            workingHour.DoctorId = user.Id;
                        }
                        // Add woking hours to doctor
                        var workingHours = _mapper.Map<IEnumerable<DoctorWorkingHours>>(createUserDto.DoctorWorkingHours);
                        await _adminRepository.AddDoctorWorkingHours(workingHours);
                        var adminResult = await _adminRepository.SaveAllAsync();
                        if (!adminResult) throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add working hours");
                    }

                    // Add Doctor service
                    if (user.DoctorSpecialityId.HasValue)
                    {
                        var servicesWithSpecialityId = await _serviceRepository.GetServicesIdsBySpecialityId(user.DoctorSpecialityId.Value);
                        if (!servicesWithSpecialityId.Any())
                        {
                            scope.Complete();
                            return _mapper.Map<UserInfoDto>(user);
                        }

                        await _doctorServiceRepository.CreateDoctorServicesForDoctor(user.Id, servicesWithSpecialityId);
                        bool saveDoctorService = await _serviceRepository.SaveAllAsync();
                        if (saveDoctorService)
                        {
                            scope.Complete();
                            return _mapper.Map<UserInfoDto>(user);
                        }
                        else throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add doctor services");
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

        public async Task ToggleIsUserDisabled(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            var existingClaim = await _userManager.GetClaimsAsync(user);
            var IsUserDisabled = existingClaim.FirstOrDefault(c => c.Type == "IsUserDisabled");
            if (IsUserDisabled == null) await _userManager.AddClaimAsync(user, new Claim("IsUserDisabled", "true"));
            else
            {
                // If the user has an IsUserDisabled claim, replace it with a new one that has the opposite value
                var newClaim = new Claim("IsUserDisabled", IsUserDisabled.Value == "true" ? "false" : "true");
                await _userManager.ReplaceClaimAsync(user, IsUserDisabled, newClaim);
            }
        }
    }
}