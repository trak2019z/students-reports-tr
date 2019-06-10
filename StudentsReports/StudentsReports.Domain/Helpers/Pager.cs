using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.Domain.Helpers
{
    public class Pager
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public string SearchExpression { get; set; }
        public string SortExpression { get; set; }
        public string SortDirection { get; set; }
    }
}
