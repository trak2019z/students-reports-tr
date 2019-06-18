using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class UpdateReport
    {
        [Required]
        public string Mark { get; set; }

        public string Comment { get; set; }
    }
}
