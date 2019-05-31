using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class StudentCourses
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }

        public Courses Course { get; set; }
        public Users User { get; set; }
    }
}
