using API.Models.Entities;
using HospitalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace Hospital.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.DoctorSpeciality)
                .WithMany(s => s.Doctors)
                .HasForeignKey(a => a.DoctorSpecialityId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AppointmentSpeciality)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.AppointmentSpecialityId);

            // Configure the relationship for BookedAppointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany(u => u.BookedAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure the relationship for Appointments where the user is the doctor
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.BookedWithAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;

            ChangeTracker.DetectChanges();
            foreach (var item in ChangeTracker.Entries()
                               .Where(i => i.State == EntityState.Added || i.State == EntityState.Modified)
                               .Where(i => i.Entity is ITrackableEntity))
            {
                if (item.State == EntityState.Added)
                {
                    item.Property("DateCreated").CurrentValue = now;
                }
                item.Property("DateUpdated").CurrentValue = now;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}