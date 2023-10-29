using API.Models.Entities;
using HospitalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public DbSet<DoctorWorkingHours> DoctorWorkingHours { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<SupplyOrder> SupplyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }
        public DbSet<ServiceInventoryItem> ServiceInventoryItems { get; set; }
        public DbSet<InvoiceDoctorService> InvoiceDoctorService { get; set; }
        public DbSet<InvoiceDoctorServiceSupplyOrders> InvoiceDoctorServiceSupplyOrders { get; set; }
        public DbSet<SellOrderConsumesSupplyOrder> SellOrderConsumesSupplyOrders { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<InvoiceMedicine> InvoiceMedicine { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureDecimalProperties(modelBuilder);
            DateTimeToPostgresSql(modelBuilder);
            
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

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.BookedAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.BookedWithAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomItem>()
                .HasOne(ci => ci.Invoice)      // Each CustomItem has one Invoice
                .WithMany(i => i.CustomItems)  // Each Invoice has many CustomItems
                .HasForeignKey(ci => ci.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Service)
                .WithMany(s => s.DoctorServices)
                .HasForeignKey(ds => ds.ServiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoctorWorkingHours>()
                .HasOne(ids => ids.Doctor)
                .WithMany(ds => ds.DoctorWorkingHours)
                .HasForeignKey(ids => ids.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplyOrder>()
                .HasOne(so => so.InventoryItem)
                .WithMany(ii => ii.SupplyOrders)
                .HasForeignKey(so => so.InventoryItemId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SellOrder>()
                .HasOne(so => so.InventoryItem)
                .WithMany(ii => ii.SellOrders)
                .HasForeignKey(so => so.InventoryItemId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ServiceInventoryItem>()
                .HasKey(sm => new { sm.ServiceId, sm.InventoryItemId });
            modelBuilder.Entity<ServiceInventoryItem>()
                .HasOne(sii => sii.InventoryItem)
                .WithMany(ii => ii.ServiceInventoryItems)
                .HasForeignKey(sii => sii.InventoryItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ServiceInventoryItem>()
                .HasOne(sii => sii.Service)
                .WithMany(s => s.ServiceInventoryItems)
                .HasForeignKey(sii => sii.ServiceId);

            modelBuilder.Entity<InvoiceDoctorServiceSupplyOrders>()
                .HasOne(sii => sii.SupplyOrder)
                .WithMany(ii => ii.InvoiceDoctorServiceSupplyOrders)
                .HasForeignKey(sii => sii.SupplyOrderId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<InvoiceDoctorServiceSupplyOrders>()
                .HasOne(sii => sii.InvoiceDoctorService)
                .WithMany(s => s.InvoiceDoctorServiceSupplyOrders)
                .HasForeignKey(sii => sii.InvoiceDoctorServiceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SellOrderConsumesSupplyOrder>()
                .HasOne(socso => socso.SellOrder)
                .WithMany(s => s.SellOrderConsumesSupplyOrders)
                .HasForeignKey(socso => socso.SellOrderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<SellOrderConsumesSupplyOrder>()
                .HasOne(socso => socso.SupplyOrder)
                .WithMany(s => s.SellOrderConsumesSupplyOrders)
                .HasForeignKey(socso => socso.SupplyOrderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InvoiceDoctorService>()
                .HasOne(sii => sii.Invoice)
                .WithMany(ii => ii.InvoiceDoctorService)
                .HasForeignKey(sii => sii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<InvoiceDoctorService>()
                .HasOne(sii => sii.DoctorService)
                .WithMany(s => s.InvoiceDoctorService)
                .HasForeignKey(sii => sii.DoctorServiceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.InventoryItemSpeciality)
                .WithMany(s => s.InventoryItems)
                .HasForeignKey(ii => ii.InventoryItemSpecialityId);

            modelBuilder.Entity<ClinicDoctor>()
                .HasKey(am => new { am.ClinicId, am.DoctorId });
            modelBuilder.Entity<ClinicDoctor>()
                .HasOne(am => am.Clinic)
                .WithMany(a => a.ClinicDoctors)
                .HasForeignKey(am => am.ClinicId);
            modelBuilder.Entity<ClinicDoctor>()
                .HasOne(am => am.Doctor)
                .WithMany(m => m.ClinicDoctors)
                .HasForeignKey(am => am.DoctorId);

            modelBuilder.Entity<InvoiceMedicine>()
                .HasKey(am => new { am.InvoiceId, am.MedicineId });
            modelBuilder.Entity<InvoiceMedicine>()
                .HasOne(am => am.Invoice)
                .WithMany(a => a.InvoiceMedicines)
                .HasForeignKey(am => am.InvoiceId);
            modelBuilder.Entity<InvoiceMedicine>()
                .HasOne(am => am.Medicine)
                .WithMany(m => m.InvoiceMedicines)
                .HasForeignKey(am => am.MedicineId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany(u => u.Images)
                .HasForeignKey(i => i.UserId);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Use the static method ToSnakeCase defined below
                var tableName = ToSnakeCase(entityType.GetTableName());
                modelBuilder.Entity(entityType.ClrType).ToTable(tableName);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

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
        private static void ConfigureDecimalProperties(ModelBuilder modelBuilder)
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
        private static void DateTimeToPostgresSql(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }
        }
        private static string ToSnakeCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }

}