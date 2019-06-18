using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class ReportsView
    {
        public int TotalItems { get; set; }
        public IEnumerable<Reports> Items { get; set; }

        public class Reports
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int SubjectId { get; set; }
            public string Mark { get; set; }
            public string Comment { get; set; }
            public string FilePath { get; set; }
        }
    }
}
