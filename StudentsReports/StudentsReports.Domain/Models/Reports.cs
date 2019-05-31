using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class Reports
    {
        public Reports()
        {
            ReportUsers = new HashSet<ReportUsers>();
        }

        public int Id { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public string Mark { get; set; }

        public Subjects Subject { get; set; }
        public ICollection<ReportUsers> ReportUsers { get; set; }
    }
}
