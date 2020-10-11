using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatHutWebsite.Models
{
    public class IdentityHelper
    {
        // roles
        public const string Administrator = "administrator";
        public const string Customer = "customer";

        /// <summary>
        /// Options for Identity are set
        /// </summary>
        /// <param name="options"></param>
        public static void SetIdentityOptions(IdentityOptions options)
        {
            // sign in options
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // password options
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;

            // lockout options
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 5;
        }

        /// <summary>
        /// Creates roles if passed in as strings
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach ( string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if ( !doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultAdministrator(IServiceProvider provider)
        {
            const string email = "administrator@rathut.com";
            const string username = "administrator";
            const string password = "password";

            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();

            if (userManager.Users.Count() == 0)
            {
                IdentityUser administrator = new IdentityUser()
                {
                    Email = email,
                    UserName = username
                };

                // creating the administrator
                await userManager.CreateAsync(administrator, password);
                await userManager.AddToRoleAsync(administrator, Administrator);
            }
        }
    }
}
