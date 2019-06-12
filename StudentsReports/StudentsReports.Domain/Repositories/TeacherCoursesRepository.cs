using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsReports.Domain.Repositories
{
    public class TeacherCoursesRepository : ITeacherCoursesRepository
    {
        private StudentsReportsContext _context;

        public TeacherCoursesRepository(StudentsReportsContext context)
        {
            _context = context;
        }

        public void Add(TeacherCourses course)
        {
            _context.Add(course);
            _context.SaveChanges();
        }

        public bool Exists(TeacherCourses course, int? id = null)
        {
            var result = _context.TeacherCourses.Where(x => x.UserId == course.UserId &&
                    x.CourseId == course.CourseId && x.CourseTypeId == course.CourseTypeId);

            if (id.HasValue)
            {
                result = result.Where(x => x.Id != id.Value);
            }

            return result.Any();
        }

        public TeacherCourses GetById(int id)
        {
            return _context.TeacherCourses.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TeacherCourses course)
        {
            _context.Update(course);
            _context.SaveChanges();
        }
    }
}
