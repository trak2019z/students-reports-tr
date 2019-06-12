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
    public class UsersRepository : IUsersRepository
    {
        private UserManager<Users> _usersManager;
        private RoleManager<IdentityRole> _roleManager;
        private IdentityStudentsReportsContext _context;

        public UsersRepository(UserManager<Users> usersManager, RoleManager<IdentityRole> roleManager,
                               IdentityStudentsReportsContext context)
        {
            this._usersManager = usersManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public async Task Add(Users user, string password, string roleId)
        {
            var result = await _usersManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role != null)
                {
                    await _usersManager.AddToRoleAsync(user, role.Name);
                }
            }
        }

        public async Task Delete(Users user)
        {
            await _usersManager.DeleteAsync(user);
        }

        public IEnumerable<UsersView> GetAll(Pager pager)
        {
            var records = (from u in _context.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.Id
                           select new 
                           {
                               Id = u.Id,
                               UserName = u.UserName,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Role = r.Name
                           });

            var result = records.GroupBy(x => x.Id).Select(x=> new UsersView
            {
                Id = x.Key,
                UserName = x.First().UserName,
                FirstName = x.First().FirstName,
                LastName = x.First().LastName,
                Roles = x.Select(y=> y.Role)
            });

            pager.TotalItems = result.Count();

            if (!String.IsNullOrEmpty(pager.SearchExpression))
            {
                result = result.Where(x => x.UserName.Contains(pager.SearchExpression.Trim()));
            }

            result = result
                    .Skip(pager.PageSize * (pager.CurrentPage - 1))
                    .Take(pager.PageSize);

            if (pager.SortDirection == "ASC")
            {
                result = result.OrderBy(x => x.GetType().GetProperty(pager.SortExpression).GetValue(x));
            }
            else
            {
                result = result.OrderByDescending(x => x.GetType().GetProperty(pager.SortExpression).GetValue(x));
            }

            return result.ToList();
        }

        public async Task<Users> GetById(string id)
        {
            return await _usersManager.FindByIdAsync(id);
        }

        public async Task<Users> GetByName(string userName)
        {
            return await _usersManager.FindByNameAsync(userName);
        }

        public async Task<UserDetails> GetDetails(string id)
        {
            var user = await _usersManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var userRole = await _usersManager.GetRolesAsync(user);

            return new UserDetails
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = userRole
            };
        }

        public async Task Update(Users user, string roleId)
        {
            var result = await _usersManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                bool userIsInRole = await _usersManager.IsInRoleAsync(user, role.Name);

                if (role != null && !userIsInRole)
                {
                    var userRoles = await _usersManager.GetRolesAsync(user);

                    await _usersManager.RemoveFromRolesAsync(user, userRoles);

                    await _usersManager.AddToRoleAsync(user, role.Name);
                }
            }
        }

        public async Task<bool> ChangePassword(Users user, string currentPassword, string newPassword)
        {          
            var result =  await _usersManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result.Succeeded;
        }

        public async Task<bool> CheckPassword(Users user, string currentPassword)
        {
            if (!await _usersManager.CheckPasswordAsync(user, currentPassword))
            {
                return false;
            }

            return true;
        }
    }
}
