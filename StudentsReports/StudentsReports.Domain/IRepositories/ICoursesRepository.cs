using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.IRepositories
{
    public interface ICoursesRepository
    {
        void Add(Courses course);
        void Update(Courses course);
        IEnumerable<Courses> GetAll();
        void Delete(Courses course);
        Courses GetById(int id);
        Courses GetByName(string name);
    }
}
