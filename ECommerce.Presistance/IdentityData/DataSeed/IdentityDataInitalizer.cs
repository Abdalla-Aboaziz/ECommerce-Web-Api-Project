using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.IdentityData.DataSeed
{
    public class IdentityDataInitalizer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataInitalizer( UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
           _roleManager = roleManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser
                    {
                        DisplayName = "Abdalla Aboaziz",
                        UserName = "AbdallaAboaziz",
                        Email = "abdallaaboaziz@gmail.com",
                        PhoneNumber = "01277353904",

                    };
                    var user02 = new ApplicationUser
                    {
                        DisplayName = "Ahmed ali",
                        UserName = "AhmedAli",
                        Email = "AhmedAli@gmail.com",
                        PhoneNumber = "01277353111",
                    };
                    // Create Users
                    await _userManager.CreateAsync(user01, "Admin@123");
                    await _userManager.CreateAsync(user02, "Admin@123");
                    // Assign Roles
                    await _userManager.AddToRoleAsync(user01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(user02, "Admin");

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error While Seeding Data {ex}");
            }
        }
    }
}
