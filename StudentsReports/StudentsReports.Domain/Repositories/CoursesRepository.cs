using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsReports.Domain.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private StudentsReportsContext _context;

        public CoursesRepository(StudentsReportsContext context)
        {
            _context = context;
        }

        public void Add(Courses course)
        {
            _context.Add(course);
            _context.SaveChanges();
        }

        public void Delete(Courses course)
        {
            _context.Remove(course);
            _context.SaveChanges();
        }

        public IEnumerable<Courses> GetAll()
        {
            return _context.Courses.AsEnumerable();
        }

        public Courses GetById(int id)
        {
            return _context.Courses.FirstOrDefault(x => x.Id == id);
        }

        public Courses GetByName(string name)
        {
            return _context.Courses.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public void Update(Courses course)
        {
            _context.Update(course);
            _context.SaveChanges();
        }
    }
}
