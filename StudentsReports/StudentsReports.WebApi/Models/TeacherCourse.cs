using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class TeacherCourse
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int CourseTypeId { get; set; }
    }
}
