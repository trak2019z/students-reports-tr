using Microsoft.AspNetCore.Identity;
using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.IRepositories
{
    public interface IRolesRepository
    {         
        Task<IdentityRole> GetById(string id);
    }
}
