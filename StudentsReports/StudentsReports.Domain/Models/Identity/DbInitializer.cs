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
               if (!await roleManager.RoleExistsAsync(UserType.Administrator.ToString()))
               {
                    await roleManager.CreateAsync(new Roles(UserType.Administrator.ToString()));
               }

                if (!await roleManager.RoleExistsAsync(UserType.Teacher.ToString()))
                {
                    await roleManager.CreateAsync(new Roles(UserType.Teacher.ToString()));
                }

                if (!await roleManager.RoleExistsAsync(UserType.Student.ToString()))
                {
                    await roleManager.CreateAsync(new Roles(UserType.Student.ToString()));
                }


                if(await userManager.FindByNameAsync("administrator") == null)
                {
                    Users user = new Users()
                    {

                        UserName = "administrator",
                        FirstName = "Administrator",
                        LastName = "Administrator"
                    };

                   var result =  await userManager.CreateAsync(user, "bu^5@GA.7sBn5S#P$cb@");

                    if (result.Succeeded)
                    {
                       await userManager.AddToRoleAsync(user, UserType.Administrator.ToString());
                    }
                }
            }
        }
    }
}
