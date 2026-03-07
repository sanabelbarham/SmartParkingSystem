using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilis
{
    public class SeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(RoleManager<IdentityRole> roleManager)
        {
            //use it to access roles table directly and to use the creatAsync and Any methods 
            _roleManager = roleManager;
        }

        async Task ISeedData.SeedData()
        {
            string[] roles = ["SuperAdmin", "Admin", "User"];
            if (!_roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                  await  _roleManager.CreateAsync(new IdentityRole(role));
                }
               
            }



        }
    }
}
