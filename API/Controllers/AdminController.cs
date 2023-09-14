using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Policy = Polices.RequireReceptionistRole)]
    public class AdminController : BaseApiController
    {
        private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
