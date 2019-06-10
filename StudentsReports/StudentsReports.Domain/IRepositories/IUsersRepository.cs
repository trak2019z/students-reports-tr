using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.IRepositories
{
    public interface IUsersRepository
    {
        Task Add(Users user, string password, string roleId);
        IEnumerable<UsersView> GetAll(Pager pager);
        Task Update(Users user, string roleId);
        Task Delete(Users user);
        Task<Users> GetById(string id);
        Task<Users> GetByName(string userName);
        Task<UserDetails> GetDetails(string id);
        Task<bool> ChangePassword(Users user, string currentPassword, string newPassword);
        Task<bool> CheckPassword(Users user, string currentPassword);
    }
}
