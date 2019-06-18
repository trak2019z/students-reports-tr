using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class CreateReport
    {
        [Required]
        public int SubjectId { get; set; }

        [Required]
        public List<string> StudentsIds { get; set; }

        public IFormFile File { get; set; }
    }
}
