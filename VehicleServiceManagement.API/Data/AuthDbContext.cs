using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Role_admin_Id = "1b104195-a0ac-42c1-824c-91df4465b044"; // Admin Role
            var Role_ServiceAdvisor_Id = "c1dc5ee9-a24c-41e4-8638-8ccbc22cd14a";  // Service Advisor Role
            var Role_Customer_Id = "5af9d363-5cc8-4613-b8f1-004746c150d1"; // Customer Role



            //---------------------- Create Roles -------------------------------------------------------

            var roles = new List<IdentityRole>()
            { 
                new IdentityRole()
                {
                     Id = Role_admin_Id,
                     Name = "Admin",
                     NormalizedName ="Admin".ToUpper(),
                     ConcurrencyStamp =Role_admin_Id
                },
                new IdentityRole()
                {
                     Id = Role_ServiceAdvisor_Id,
                     Name = "Service Advisor",
                     NormalizedName ="Service Advisor".ToUpper(),
                     ConcurrencyStamp =Role_ServiceAdvisor_Id
                },
                new IdentityRole()
                {
                     Id = Role_Customer_Id,
                     Name = "Customer",
                     NormalizedName ="Customer".ToUpper(),
                     ConcurrencyStamp =Role_Customer_Id
                }
            };

            // Seed the roles 

            builder.Entity<IdentityRole>().HasData(roles);

            //--------------------------- Create Users ------------------------------

            var UserAdmin_id = "de255926-10e1-4e02-9389-38d2e3bc505d"; // Admin User
            var UserSa_id = "8d16201a-dcbb-49f6-b320-3ca25615a161"; // Service Advisor User

            var users = new List<IdentityUser>()
            {
                new IdentityUser() // admin
                {
                     Id = UserAdmin_id,
                     Email = "admin@gmail.com",
                     NormalizedEmail = "admin@gmail.com".ToUpper(),
                     UserName = "admin@gmail.com",
                     NormalizedUserName ="admin@gmail.com".ToUpper()
                },
                 new IdentityUser() // service advisor
                {
                     Id = UserSa_id,
                     Email = "sa@gmail.com",
                     NormalizedEmail = "sa@gmail.com".ToUpper(),
                     UserName = "sa@gmail.com",
                     NormalizedUserName ="sa@gmail.com".ToUpper()
                }
            };


            var userAdmin1 =  users.ElementAtOrDefault(0); // ADMIN
            var userSa2 = users.ElementAtOrDefault(1); // Service Advisor

            userAdmin1.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(userAdmin1, "Admin@123");
            userSa2.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(userSa2, "Sa@123");

            //foreach (var user in users)
            //{
            //    user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "Admin@123"); // For both the user password will be --> Admin@123
            //}

            builder.Entity<IdentityUser>().HasData(users);

            //----------------- Give the roles to Users ----------------------

            var userAdminRole = new IdentityUserRole<string>()
            {
                 RoleId = Role_admin_Id,
                 UserId = UserAdmin_id
            };

            var userSaRole = new IdentityUserRole<string>()
            {
                RoleId = Role_ServiceAdvisor_Id,
                UserId = UserSa_id
            };

            var userRoles = new List<IdentityUserRole<string>>()
            {
                userAdminRole,
                userSaRole
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }


    }
}
