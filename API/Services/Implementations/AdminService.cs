using System.Transactions;
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
        public AdminService(UserManager<AppUser> userManager, IMapper mapper, IAuthenticationService authenticationService, IAdminRepository adminRepository)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserDto> CreateUser(CreateUserDto createUserDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (await _authenticationService.UserExists(createUserDto.Username))
                        throw new ApiException(400, "Username is already taken");

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

                    var result = await _userManager.CreateAsync(user, createUserDto.Password);
                    if (!result.Succeeded)
                    {
                        throw new ApiException(500, "Failed to create user");
                    }
                    else
                    {
                        createUserDto.Id = user.Id;
                    }

                    if (!Roles.IsValidRole(createUserDto.Role))
                        throw new ApiException(400, "Invalid role specified");

                    var roleResults = await _userManager.AddToRoleAsync(user, createUserDto.Role);
                    if (!roleResults.Succeeded)
                        throw new ApiException(500, "Failed to add role");

                    if (createUserDto.Role == Roles.Doctor)
                    {
                        if (createUserDto.DoctorWorkingHours != null)
                        {
                            foreach (var workingHour in createUserDto.DoctorWorkingHours)
                            {
                                workingHour.DoctorId = createUserDto.Id;
                            }
                        }
                        var workingHours = _mapper.Map<IEnumerable<DoctorWorkingHours>>(createUserDto.DoctorWorkingHours);
                        await _adminRepository.AddDoctorWorkingHours(workingHours);
                        var adminResult = await _adminRepository.SaveAllAsync();
                        if(!adminResult) throw new ApiException(500, "Failed to add working hours");

                    }

                    scope.Complete(); // Mark the transaction as complete, to be committed
                }
                catch (Exception)
                {
                    // No need to explicitly rollback, TransactionScope will handle it automatically
                    throw; // Re-throw the exception for further handling
                }
            }

            return createUserDto;
        }

    }
}