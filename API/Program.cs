using System.Text;
using API.Helpers;
using API.Middleware;
using API.Repositories.Implementations;
using API.Repositories.Interfaces;
using API.Services.Implementations;
using API.Services.Interfaces;
using Hospital.Data;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using webapi.Data;
using webapi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<AppRole>()
.AddRoleManager<RoleManager<AppRole>>()
.AddSignInManager<SignInManager<AppUser>>()
.AddRoleValidator<RoleValidator<AppRole>>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(Polices.RequireAdminRole, policy => policy.RequireRole(Roles.Admin));
    opt.AddPolicy(Polices.RequireReceptionistRole, policy =>
    {
        policy.RequireRole(Roles.Admin, Roles.Receptionist);
    });
    opt.AddPolicy(Polices.RequireDoctorRole, policy =>
    {
        policy.RequireRole(Roles.Admin, Roles.Receptionist, Roles.Doctor);
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put Bearer + your token in the box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppoinmentRepository, AppoinmentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppoinmentService, AppoinmentService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IDoctorServiceService, DoctorServiceService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddScoped<Seed>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.UseItToSeedSqlServerAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(policy => policy.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("http://localhost:4200"));
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
