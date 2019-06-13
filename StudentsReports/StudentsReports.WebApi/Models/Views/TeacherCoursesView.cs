using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class TeacherCoursesView
    {
        public int TotalItems { get; set; }
        public IEnumerable<TeacherCourses> Items { get; set; }

        public class TeacherCourses
        {
            public int Id { get; set; }
            public int CourseId { get; set; }
            public string CourseName { get; set; }
            public int CourseTypeId { get; set; }
            public string CourseTypeName { get; set; }
            public string UserId { get; set; }
            public string UserFullName { get; set; }
        }
    }
}
