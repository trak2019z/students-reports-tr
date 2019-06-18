using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.IRepositories
{
    public interface IReportsRepository
    {
        void Add(Reports record, IEnumerable<string> ids);
        void Update(Reports record);
        Reports GetById(int id);
        IEnumerable<ReportsView> GetAll(Pager pager);
    }
}
