using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.Models
{
    public class TeacherCoursesView
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
