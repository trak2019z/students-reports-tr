using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.Models
{
    public class ReportsView
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public string Mark { get; set; }
        public string Comment { get; set; }
        public string FileName { get; set; }
        public string FileId { get; set; }
    }
}
