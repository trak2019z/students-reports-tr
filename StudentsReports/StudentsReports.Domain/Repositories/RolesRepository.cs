using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.IRepositories
{
    public class RolesRepository : IRolesRepository
    {
        private RoleManager<IdentityRole> _roleManager;

        public RolesRepository(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task<IdentityRole> GetById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }
    }
}
