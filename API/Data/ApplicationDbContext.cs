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
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CustomItem> CustomItems { get; set; }
        public DbSet<DoctorService> DoctorServices { get; set; }
        public DbSet<InvoiceDoctorService> InvoiceDoctorServices  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureDecimalProperties(modelBuilder);

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

            modelBuilder.Entity<CustomItem>()
                .HasOne(ci => ci.Invoice)      // Each CustomItem has one Invoice
                .WithMany(i => i.CustomItems)  // Each Invoice has many CustomItems
                .HasForeignKey(ci => ci.InvoiceId); // Foreign key property

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Appointment)
                .WithOne(a => a.Invoice)
                .HasForeignKey<Appointment>(a => a.InvoiceId);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceSpeciality)
                .WithMany(s => s.Services)
                .HasForeignKey(a => a.ServiceSpecialityId);

            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Doctor)
                .WithMany(u => u.DoctorServices)
                .HasForeignKey(ds => ds.DoctorId)
                .IsRequired();

            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Service)
                .WithMany(s => s.DoctorServices)
                .HasForeignKey(ds => ds.ServiceId)
                .IsRequired();

            modelBuilder.Entity<InvoiceDoctorService>()
                .HasOne(ids => ids.Invoice)
                .WithMany(i => i.InvoiceDoctorServices)
                .HasForeignKey(ids => ids.InvoiceId)
                .IsRequired();

            modelBuilder.Entity<InvoiceDoctorService>()
                .HasOne(ids => ids.DoctorService)
                .WithMany(ds => ds.InvoiceDoctorServices)
                .HasForeignKey(ids => ids.DoctorServiceId)
                .IsRequired();
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
        private void ConfigureDecimalProperties(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal))
                    {
                        property.SetColumnType("decimal(18, 3)"); // Set precision to 18 and scale to 3
                    }
                }
            }
        }
    }
}