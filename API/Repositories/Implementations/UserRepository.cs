using API.Helpers;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            query = userParams.OrderBy switch
            {
                "updated" => query.OrderByDescending(u => u.DateUpdated),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<AppUser>> GetAllUsersWithRoleAsync(UserParams userParams, string roleName)
        {
            var query = await GetUsersInRoleAsync(roleName);
            query = userParams.OrderBy switch
            {
                "updated" => query.OrderByDescending(u => u.DateUpdated),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IQueryable<AppUser>> GetUsersInRoleAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (role == null)
            {
                throw new ArgumentException($"Role {roleName} does not exist");
            }

            var usersInRole = _context.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.User)
                .AsQueryable();

            return usersInRole;
        }
    }
}