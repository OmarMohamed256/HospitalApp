﻿// <auto-generated />
using System;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Entities.AppointmentMedicine", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId", "MedicineId");

                    b.HasIndex("MedicineId");

                    b.ToTable("AppointmentMedicine");
                });

            modelBuilder.Entity("API.Models.Entities.CustomItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("CustomItems");
                });

            modelBuilder.Entity("API.Models.Entities.DoctorService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<decimal>("DoctorPercentage")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("HospitalPercentage")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("ServiceId");

                    b.ToTable("DoctorServices");
                });

            modelBuilder.Entity("API.Models.Entities.DoctorWorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("DoctorWorkingHours");
                });

            modelBuilder.Entity("API.Models.Entities.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InventoryItemSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemSpecialityId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("API.Models.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<decimal>("AppointmentTypePrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("CustomItemsTotalPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<DateTime>("FinalizationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAfterDiscount")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalDue")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalPaid")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalRemaining")
                        .HasColumnType("decimal(18, 3)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("API.Models.Entities.InvoiceDoctorService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DoctorServiceId")
                        .HasColumnType("int");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceQuantity")
                        .HasColumnType("int");

                    b.Property<decimal>("ServiceSoldPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalDisposablesPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorServiceId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceDoctorService");
                });

            modelBuilder.Entity("API.Models.Entities.InvoiceDoctorServiceSupplyOrders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("InvoiceDoctorServiceId")
                        .HasColumnType("int");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<int>("QuantityUsed")
                        .HasColumnType("int");

                    b.Property<int?>("SupplyOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceDoctorServiceId");

                    b.HasIndex("SupplyOrderId");

                    b.ToTable("InvoiceDoctorServiceSupplyOrders");
                });

            modelBuilder.Entity("API.Models.Entities.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("API.Models.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomSpecialityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId")
                        .IsUnique()
                        .HasFilter("[DoctorId] IS NOT NULL");

                    b.HasIndex("RoomSpecialityId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("API.Models.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceSpecialityId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceSpecialityId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("API.Models.Entities.ServiceInventoryItem", b =>
                {
                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNeeded")
                        .HasColumnType("int");

                    b.HasKey("ServiceId", "InventoryItemId");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("ServiceInventoryItems");
                });

            modelBuilder.Entity("API.Models.Entities.SupplyOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("SupplyOrders");
                });

            modelBuilder.Entity("HospitalApp.Models.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("CreationNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfVisit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnoses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentSpecialityId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("InvoiceId")
                        .IsUnique()
                        .HasFilter("[InvoiceId] IS NOT NULL");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HospitalApp.Models.Entities.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("webapi.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("webapi.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<decimal?>("PriceRevisit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceVisit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorSpecialityId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("webapi.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("API.Models.Entities.AppointmentMedicine", b =>
                {
                    b.HasOne("HospitalApp.Models.Entities.Appointment", "Appointment")
                        .WithMany("AppointmentMedicines")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Entities.Medicine", "Medicine")
                        .WithMany("AppointmentMedicines")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("API.Models.Entities.CustomItem", b =>
                {
                    b.HasOne("API.Models.Entities.Invoice", "Invoice")
                        .WithMany("CustomItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("API.Models.Entities.DoctorService", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", "Doctor")
                        .WithMany("DoctorServices")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Entities.Service", "Service")
                        .WithMany("DoctorServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("API.Models.Entities.DoctorWorkingHours", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", "Doctor")
                        .WithMany("DoctorWorkingHours")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Models.Entities.InventoryItem", b =>
                {
                    b.HasOne("HospitalApp.Models.Entities.Speciality", "InventoryItemSpeciality")
                        .WithMany("InventoryItems")
                        .HasForeignKey("InventoryItemSpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItemSpeciality");
                });

            modelBuilder.Entity("API.Models.Entities.InvoiceDoctorService", b =>
                {
                    b.HasOne("API.Models.Entities.DoctorService", "DoctorService")
                        .WithMany("InvoiceDoctorService")
                        .HasForeignKey("DoctorServiceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("API.Models.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceDoctorService")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoctorService");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("API.Models.Entities.InvoiceDoctorServiceSupplyOrders", b =>
                {
                    b.HasOne("API.Models.Entities.InvoiceDoctorService", "InvoiceDoctorService")
                        .WithMany("InvoiceDoctorServiceSupplyOrders")
                        .HasForeignKey("InvoiceDoctorServiceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("API.Models.Entities.SupplyOrder", "SupplyOrder")
                        .WithMany("InvoiceDoctorServiceSupplyOrders")
                        .HasForeignKey("SupplyOrderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("InvoiceDoctorService");

                    b.Navigation("SupplyOrder");
                });

            modelBuilder.Entity("API.Models.Entities.Room", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", "Doctor")
                        .WithOne()
                        .HasForeignKey("API.Models.Entities.Room", "DoctorId");

                    b.HasOne("HospitalApp.Models.Entities.Speciality", "Speciality")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomSpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("API.Models.Entities.Service", b =>
                {
                    b.HasOne("HospitalApp.Models.Entities.Speciality", "ServiceSpeciality")
                        .WithMany("Services")
                        .HasForeignKey("ServiceSpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceSpeciality");
                });

            modelBuilder.Entity("API.Models.Entities.ServiceInventoryItem", b =>
                {
                    b.HasOne("API.Models.Entities.InventoryItem", "InventoryItem")
                        .WithMany("ServiceInventoryItems")
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Models.Entities.Service", "Service")
                        .WithMany("ServiceInventoryItems")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItem");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("API.Models.Entities.SupplyOrder", b =>
                {
                    b.HasOne("API.Models.Entities.InventoryItem", "InventoryItem")
                        .WithMany("SupplyOrders")
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("InventoryItem");
                });

            modelBuilder.Entity("HospitalApp.Models.Entities.Appointment", b =>
                {
                    b.HasOne("HospitalApp.Models.Entities.Speciality", "AppointmentSpeciality")
                        .WithMany("Appointments")
                        .HasForeignKey("AppointmentSpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Entities.AppUser", "Doctor")
                        .WithMany("BookedWithAppointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("API.Models.Entities.Invoice", "Invoice")
                        .WithOne("Appointment")
                        .HasForeignKey("HospitalApp.Models.Entities.Appointment", "InvoiceId");

                    b.HasOne("webapi.Entities.AppUser", "Patient")
                        .WithMany("BookedAppointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppointmentSpeciality");

                    b.Navigation("Doctor");

                    b.Navigation("Invoice");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("webapi.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("webapi.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapi.Entities.AppUser", b =>
                {
                    b.HasOne("HospitalApp.Models.Entities.Speciality", "DoctorSpeciality")
                        .WithMany("Doctors")
                        .HasForeignKey("DoctorSpecialityId");

                    b.Navigation("DoctorSpeciality");
                });

            modelBuilder.Entity("webapi.Entities.AppUserRole", b =>
                {
                    b.HasOne("webapi.Entities.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Entities.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Models.Entities.DoctorService", b =>
                {
                    b.Navigation("InvoiceDoctorService");
                });

            modelBuilder.Entity("API.Models.Entities.InventoryItem", b =>
                {
                    b.Navigation("ServiceInventoryItems");

                    b.Navigation("SupplyOrders");
                });

            modelBuilder.Entity("API.Models.Entities.Invoice", b =>
                {
                    b.Navigation("Appointment")
                        .IsRequired();

                    b.Navigation("CustomItems");

                    b.Navigation("InvoiceDoctorService");
                });

            modelBuilder.Entity("API.Models.Entities.InvoiceDoctorService", b =>
                {
                    b.Navigation("InvoiceDoctorServiceSupplyOrders");
                });

            modelBuilder.Entity("API.Models.Entities.Medicine", b =>
                {
                    b.Navigation("AppointmentMedicines");
                });

            modelBuilder.Entity("API.Models.Entities.Service", b =>
                {
                    b.Navigation("DoctorServices");

                    b.Navigation("ServiceInventoryItems");
                });

            modelBuilder.Entity("API.Models.Entities.SupplyOrder", b =>
                {
                    b.Navigation("InvoiceDoctorServiceSupplyOrders");
                });

            modelBuilder.Entity("HospitalApp.Models.Entities.Appointment", b =>
                {
                    b.Navigation("AppointmentMedicines");
                });

            modelBuilder.Entity("HospitalApp.Models.Entities.Speciality", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Doctors");

                    b.Navigation("InventoryItems");

                    b.Navigation("Rooms");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("webapi.Entities.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("webapi.Entities.AppUser", b =>
                {
                    b.Navigation("BookedAppointments");

                    b.Navigation("BookedWithAppointments");

                    b.Navigation("DoctorServices");

                    b.Navigation("DoctorWorkingHours");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
