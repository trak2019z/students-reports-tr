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
        private IdentityStudentsReportsContext _identityContext;

        public TeacherCoursesRepository(StudentsReportsContext context, IdentityStudentsReportsContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
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

        public IEnumerable<TeacherCoursesView> GetAll(Pager pager)
        {
            var users = _identityContext.Users.AsEnumerable();

            var result = (from t in _context.TeacherCourses
                          join c in _context.Courses on t.CourseId equals c.Id
                          join ct in _context.CoursesTypes on t.CourseTypeId equals ct.Id
                          select new TeacherCoursesView
                          {
                              Id = t.Id,
                              CourseId = t.CourseId,
                              CourseName = c.Name,
                              CourseTypeId = t.CourseTypeId,
                              CourseTypeName = ct.Name,
                              UserId = t.UserId,
                              UserFullName = users.Single(x => x.Id == t.UserId).FirstName + " " + users.Single(x => x.Id == t.UserId).LastName
                           });

            pager.TotalItems = result.Count();

            if (!String.IsNullOrEmpty(pager.SearchExpression))
            {
                result = result.Where(x => x.CourseName.Contains(pager.SearchExpression.Trim()) 
                || x.CourseTypeName.Contains(pager.SearchExpression.Trim()));
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
