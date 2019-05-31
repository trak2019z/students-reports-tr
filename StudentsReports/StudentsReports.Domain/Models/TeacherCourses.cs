using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class TeacherCourses
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public int CourseTypeId { get; set; }

        public Courses Course { get; set; }
        public CoursesTypes CourseType { get; set; }
        public Users User { get; set; }
    }
}
