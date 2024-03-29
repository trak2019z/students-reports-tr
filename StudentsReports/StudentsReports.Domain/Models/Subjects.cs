﻿using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            Reports = new HashSet<Reports>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeacherCourseId { get; set; }

        public TeacherCourses TeacherCourse { get; set; }
        public ICollection<Reports> Reports { get; set; }
    }
}
