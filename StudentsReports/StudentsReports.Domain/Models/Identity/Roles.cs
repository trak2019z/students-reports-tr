using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.Models
{
    public class Roles : IdentityRole
    {
        public Roles(string roleName) : base(roleName)
        {
        }
    }
}
