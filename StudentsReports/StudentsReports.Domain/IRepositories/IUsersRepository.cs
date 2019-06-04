using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsReports.Domain.IRepositories
{
    public interface IUsersRepository
    {
        Task Add(Users user);
        List<Users> GetAll();
        Task Update(Users user);
        Task Delete(Users user);
        Users GetById(int id);
    }
}
