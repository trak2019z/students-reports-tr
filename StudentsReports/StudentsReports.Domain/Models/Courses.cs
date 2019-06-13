using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class Courses
    {
        public Courses()
        {
            TeacherCourses = new HashSet<TeacherCourses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeacherCourses> TeacherCourses { get; set; }
    }
}
