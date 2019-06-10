using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.Models.Identity
{
    public class DbInitializer
    {
        public static async Task Seed(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
               if (!await roleManager.RoleExistsAsync(UserRoles.Administrator))
               {
                    await roleManager.CreateAsync(new Roles(UserRoles.Administrator));
               }

                if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                {
                    await roleManager.CreateAsync(new Roles(UserRoles.Teacher));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                {
                    await roleManager.CreateAsync(new Roles(UserRoles.Student));
                }


                if(await userManager.FindByNameAsync("administrator") == null)
                {
                    Users user = new Users()
                    {

                        UserName = "administrator",
                        FirstName = "Administrator",
                        LastName = "Administrator",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };

                   var result =  await userManager.CreateAsync(user, "bu^5@GA.7sBn5S#P$cb@");

                    if (result.Succeeded)
                    {
                       await userManager.AddToRoleAsync(user, UserRoles.Administrator);
                    }
                }
            }
        }
    }
}
