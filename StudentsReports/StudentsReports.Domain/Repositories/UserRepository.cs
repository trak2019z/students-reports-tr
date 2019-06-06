using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.IRepositories
{
    public class UsersRepository : IUsersRepository
    {
        private UserManager<Users> _usersManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersRepository(UserManager<Users> usersManager, RoleManager<IdentityRole> roleManager)
        {
            this._usersManager = usersManager;
            this._roleManager = roleManager;
        }

        public Task Add(Users user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Users user)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAll()
        {
            return _usersManager.Users.ToList();
        }

        public Users GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
