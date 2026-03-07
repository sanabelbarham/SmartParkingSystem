using DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilis
{
  public class UserSeedData:ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser>userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedData()
        {
    
            if( ! await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "sanabel",
                    FullName = "sanabel barham",
                    Email = "sanabelbarham123@gmail.com",
                    EmailConfirmed = true

                };
                var user2 = new ApplicationUser
                {
                    UserName = "malakoot",
                    FullName = "malakoot barham",
                    Email = "malakootbarham123@gmail.com",
                    EmailConfirmed = true
                };
                var user3 = new ApplicationUser
                {
                    UserName = "sabri",
                    FullName = "sabri barham",
                    Email = "sabribarham123@gmail.com",
                    EmailConfirmed = true
                };
                var user4 = new ApplicationUser
                {
                    UserName = "yaqoot",
                    FullName = "yaqoot barham",
                    Email = "yaqootbarham123@gmail.com",
                    EmailConfirmed = true
                };

                var creatUser1 = await _userManager.CreateAsync(user1, "Sanabel@123");
                var creatUser2 = await _userManager.CreateAsync(user2, "malakoot@123");
                var creatUser3 = await _userManager.CreateAsync(user3, "sabri@123");
                var creatUser4 = await _userManager.CreateAsync(user4, "yaqoot@123");

                await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin");
                await _userManager.AddToRoleAsync(user3, "User");
                await _userManager.AddToRoleAsync(user4, "User");

            }
        }
    }

}
