using API.Helpers;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams)
        {
            var query = _context.Users
                    .Include(u => u.UserRoles)
                    .Select(u => new AppUser
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,
                        Age = u.Age,
                        DateCreated = u.DateCreated,
                        DateUpdated = u.DateUpdated,
                        DoctorSpecialityId = u.DoctorSpecialityId,
                        FullName = u.FullName,
                        PhoneNumber = u.PhoneNumber,
                        UserRoles = u.UserRoles.Select(ur => new AppUserRole
                        {
                            UserId = ur.UserId,
                            RoleId = ur.RoleId,
                            Role = ur.Role // Include if needed
                        }).ToList()
                    })
                    .AsQueryable();

            if (!string.IsNullOrEmpty(userParams.SearchTerm)) query = query.Where(u => u.FullName.Contains(userParams.SearchTerm) || u.Email.Contains(userParams.SearchTerm));
            if (!string.IsNullOrEmpty(userParams.Gender)) query = query.Where(u => u.Gender == userParams.Gender);
            if (userParams.DoctorSpecialityId != null)
                query = query.Where(u => u.DoctorSpecialityId == userParams.DoctorSpecialityId);

            query = (userParams.OrderBy, userParams.Order) switch
            {
                ("updated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("updated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("date", "asc") => query.OrderBy(u => u.DateCreated),
                ("date", "desc") => query.OrderByDescending(u => u.DateCreated),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<AppUser>> GetAllUsersWithRoleAsync(UserParams userParams, string roleName)
        {
            var query = await GetUsersInRoleAsync(roleName);

            if (!string.IsNullOrEmpty(userParams.SearchTerm)) query = query.Where(u => u.FullName.Contains(userParams.SearchTerm) || u.Email.Contains(userParams.SearchTerm));
            if (!string.IsNullOrEmpty(userParams.Gender)) query = query.Where(u => u.Gender == userParams.Gender);
            if (userParams.DoctorSpecialityId != null)
                query = query.Where(u => u.DoctorSpecialityId == userParams.DoctorSpecialityId);

            query = (userParams.OrderBy, userParams.Order) switch
            {
                ("updated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("updated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("date", "asc") => query.OrderBy(u => u.DateCreated),
                ("date", "desc") => query.OrderByDescending(u => u.DateCreated),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IQueryable<AppUser>> GetUsersInRoleAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName) ??
                throw new ArgumentException($"Role {roleName} does not exist");

            var usersInRole = _context.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.User)
                .AsQueryable();

            return usersInRole;
        }
    }
}