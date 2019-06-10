using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Helpers
{
    public class Pager
    {
        public int PageSize { get; set; } = 20;
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
        public string SearchExpression { get; set; }
        public string SortExpression { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";
    }
}
