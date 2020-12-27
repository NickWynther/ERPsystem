using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Data.DataInitializer
{

    /// <summary>
    /// This class provide methods for database initialization (seeding), if database is empty.
    /// </summary>
    public static class UserAndRoleDataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// Create admin and user examples
        /// </summary>
        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("johndoe@localhost").Result == null)
            {
                User user = new User
                {
                    UserName = "johndoe@localhost",
                    Email = "johndoe@localhost",
                    FirstName = "John",
                    LastName = "Doe"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }


            if (userManager.FindByEmailAsync("alex@localhost").Result == null)
            {
                User user = new User
                {
                    UserName = "alex@localhost",
                    Email = "alex@localhost",
                    FirstName = "Alex",
                    LastName = "Calingasan"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }


        /// <summary>
        /// Create user and admin roles
        /// </summary>
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
