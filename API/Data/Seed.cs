

using Hospital.Data;
using HospitalApp.Constants;
using HospitalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ApplicationDbContext context)
        {
            // var appointment = new Appointment{
            //     AppointmentSpecialityId = 1,
            //     DateOfVisit = DateTime.Now.AddDays(3),
            //     DoctorId = 2,
            //     PatientId = 3,
            //     Type = "revisit",
            //     Status = "cancelled",
            //     CreationNote = "Not Urgent"
            // };
            // context.Appointments.Add(appointment);
            // await context.SaveChangesAsync();
            
            if (await userManager.Users.AnyAsync()) return;

            // Seed Roles
            var roles = new List<AppRole>
            {
                new AppRole{Name=Roles.Patient},
                new AppRole{Name=Roles.Admin},
                new AppRole{Name=Roles.Doctor},
                new AppRole{Name=Roles.Receptionist}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

          //  add user
            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FullName = "admin",
                Gender = "male",
                Age = 30
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { Roles.Admin });
            await context.SaveChangesAsync();

            // var Doctor = new AppUser
            // {
            //     UserName = "doctor",
            //     Email = "doctor@gmail.com",
            //     FullName = "doctor",
            //     Gender = "male",
            //     Age = 50
            // };
            // await userManager.CreateAsync(Doctor, "Pa$$w0rd");
            // await userManager.AddToRolesAsync(Doctor, new[] { Roles.Doctor });
            // await context.SaveChangesAsync();

            // var Patient = new AppUser
            // {
            //     UserName = "patient",
            //     Email = "patient@gmail.com",
            //     FullName = "patient",
            //     Gender = "male",
            //     Age = 40

            // };
            // await userManager.CreateAsync(Patient, "Pa$$w0rd");
            // await userManager.AddToRolesAsync(Patient, new[] { Roles.Patient });
            // await context.SaveChangesAsync();

            // var Patient1 = new AppUser
            // {
            //     UserName = "patient1",
            //     Email = "patient1@gmail.com",
            //     FullName = "patient1",
            //     Gender = "female",
            //     Age = 20
            // };
            // await userManager.CreateAsync(Patient1, "Pa$$w0rd");
            // await userManager.AddToRolesAsync(Patient1, new[] { Roles.Patient });
            // await context.SaveChangesAsync();

            // var Receptionist = new AppUser
            // {
            //     UserName = "receptionist",
            //     Email = "receptionist@gmail.com",
            //     FullName = "receptionist",
            //     Gender = "male",
            //     Age = 40

            // };
            // await userManager.CreateAsync(Receptionist, "Pa$$w0rd");
            // await userManager.AddToRolesAsync(Receptionist, new[] { Roles.Receptionist });

            // await context.SaveChangesAsync();
        }
    }
}
