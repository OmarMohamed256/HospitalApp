using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HospitalApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClinicNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    TotalDue = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    CustomItemsTotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalRemaining = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    AppointmentTypePrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    FinalizationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "asp_net_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "custom_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custom_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_custom_items_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_medicine",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    DosageAmount = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_medicine", x => new { x.InvoiceId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_invoice_medicine_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoice_medicine_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    PriceVisit = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceRevisit = table.Column<decimal>(type: "numeric", nullable: true),
                    DoctorSpecialityId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asp_net_users_specialities_DoctorSpecialityId",
                        column: x => x.DoctorSpecialityId,
                        principalTable: "specialities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "inventory_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InventoryItemSpecialityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventory_items_specialities_InventoryItemSpecialityId",
                        column: x => x.InventoryItemSpecialityId,
                        principalTable: "specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    ServiceSpecialityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_services_specialities_ServiceSpecialityId",
                        column: x => x.ServiceSpecialityId,
                        principalTable: "specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfVisit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationNote = table.Column<string>(type: "text", nullable: true),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentSpecialityId = table.Column<int>(type: "integer", nullable: false),
                    Diagnoses = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointments_asp_net_users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_appointments_asp_net_users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_appointments_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_appointments_specialities_AppointmentSpecialityId",
                        column: x => x.AppointmentSpecialityId,
                        principalTable: "specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asp_net_user_claims_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_asp_net_user_logins_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "asp_net_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asp_net_user_roles_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asp_net_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clinic_doctor",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinic_doctor", x => new { x.ClinicId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_clinic_doctor_asp_net_users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clinic_doctor_clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctor_working_hours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_working_hours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctor_working_hours_asp_net_users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ImageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Organ = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_asp_net_users_UserId",
                        column: x => x.UserId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sell_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoldTo = table.Column<string>(type: "text", nullable: true),
                    IncludeExpiredItems = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SellPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sell_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sell_orders_inventory_items_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "inventory_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "supply_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    SupplierName = table.Column<string>(type: "text", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConsumedQuantity = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SellPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supply_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_supply_orders_inventory_items_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "inventory_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "doctor_services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    DoctorPercentage = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    HospitalPercentage = table.Column<decimal>(type: "numeric(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor_services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctor_services_asp_net_users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "asp_net_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_doctor_services_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_inventory_items",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: false),
                    QuantityNeeded = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_inventory_items", x => new { x.ServiceId, x.InventoryItemId });
                    table.ForeignKey(
                        name: "FK_service_inventory_items_inventory_items_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "inventory_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_service_inventory_items_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sell_order_consumes_supply_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellOrderId = table.Column<int>(type: "integer", nullable: true),
                    SupplyOrderId = table.Column<int>(type: "integer", nullable: true),
                    QuantityUsed = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sell_order_consumes_supply_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sell_order_consumes_supply_orders_sell_orders_SellOrderId",
                        column: x => x.SellOrderId,
                        principalTable: "sell_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_sell_order_consumes_supply_orders_supply_orders_SupplyOrder~",
                        column: x => x.SupplyOrderId,
                        principalTable: "supply_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "invoice_doctor_service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    DoctorServiceId = table.Column<int>(type: "integer", nullable: true),
                    ServiceQuantity = table.Column<int>(type: "integer", nullable: false),
                    TotalDisposablesPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    ServiceSoldPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_doctor_service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_doctor_service_doctor_services_DoctorServiceId",
                        column: x => x.DoctorServiceId,
                        principalTable: "doctor_services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_invoice_doctor_service_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_doctor_service_supply_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceDoctorServiceId = table.Column<int>(type: "integer", nullable: true),
                    SupplyOrderId = table.Column<int>(type: "integer", nullable: false),
                    QuantityUsed = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_doctor_service_supply_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_doctor_service_supply_orders_invoice_doctor_service~",
                        column: x => x.InvoiceDoctorServiceId,
                        principalTable: "invoice_doctor_service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_invoice_doctor_service_supply_orders_supply_orders_SupplyOr~",
                        column: x => x.SupplyOrderId,
                        principalTable: "supply_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_AppointmentSpecialityId",
                table: "appointments",
                column: "AppointmentSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_DoctorId",
                table: "appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_InvoiceId",
                table: "appointments",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointments_PatientId",
                table: "appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_role_claims_RoleId",
                table: "asp_net_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "asp_net_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_claims_UserId",
                table: "asp_net_user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_logins_UserId",
                table: "asp_net_user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_user_roles_RoleId",
                table: "asp_net_user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "asp_net_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_asp_net_users_DoctorSpecialityId",
                table: "asp_net_users",
                column: "DoctorSpecialityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "asp_net_users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clinic_doctor_DoctorId",
                table: "clinic_doctor",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_custom_items_InvoiceId",
                table: "custom_items",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_services_DoctorId",
                table: "doctor_services",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_services_ServiceId",
                table: "doctor_services",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_working_hours_DoctorId",
                table: "doctor_working_hours",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_images_UserId",
                table: "images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_items_InventoryItemSpecialityId",
                table: "inventory_items",
                column: "InventoryItemSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_doctor_service_DoctorServiceId",
                table: "invoice_doctor_service",
                column: "DoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_doctor_service_InvoiceId",
                table: "invoice_doctor_service",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_doctor_service_supply_orders_InvoiceDoctorServiceId",
                table: "invoice_doctor_service_supply_orders",
                column: "InvoiceDoctorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_doctor_service_supply_orders_SupplyOrderId",
                table: "invoice_doctor_service_supply_orders",
                column: "SupplyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_medicine_MedicineId",
                table: "invoice_medicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_sell_order_consumes_supply_orders_SellOrderId",
                table: "sell_order_consumes_supply_orders",
                column: "SellOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_sell_order_consumes_supply_orders_SupplyOrderId",
                table: "sell_order_consumes_supply_orders",
                column: "SupplyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_sell_orders_InventoryItemId",
                table: "sell_orders",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_service_inventory_items_InventoryItemId",
                table: "service_inventory_items",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_services_ServiceSpecialityId",
                table: "services",
                column: "ServiceSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_supply_orders_InventoryItemId",
                table: "supply_orders",
                column: "InventoryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "clinic_doctor");

            migrationBuilder.DropTable(
                name: "custom_items");

            migrationBuilder.DropTable(
                name: "doctor_working_hours");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "invoice_doctor_service_supply_orders");

            migrationBuilder.DropTable(
                name: "invoice_medicine");

            migrationBuilder.DropTable(
                name: "sell_order_consumes_supply_orders");

            migrationBuilder.DropTable(
                name: "service_inventory_items");

            migrationBuilder.DropTable(
                name: "asp_net_roles");

            migrationBuilder.DropTable(
                name: "clinics");

            migrationBuilder.DropTable(
                name: "invoice_doctor_service");

            migrationBuilder.DropTable(
                name: "medicines");

            migrationBuilder.DropTable(
                name: "sell_orders");

            migrationBuilder.DropTable(
                name: "supply_orders");

            migrationBuilder.DropTable(
                name: "doctor_services");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "inventory_items");

            migrationBuilder.DropTable(
                name: "asp_net_users");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "specialities");
        }
    }
}
