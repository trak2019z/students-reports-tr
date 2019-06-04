using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class ReportUsers
    {
        public int ReportId { get; set; }
        public string UserId { get; set; }

        public Reports Report { get; set; }
    }
}
