using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace StudentsReports.Domain.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private StudentsReportsContext _context;
        private IdentityStudentsReportsContext _identityContext;

        public ReportsRepository(StudentsReportsContext context, IdentityStudentsReportsContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public void Add(Reports record, IEnumerable<string> ids)
        {
            var users = new List<ReportUsers>();

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _context.Add(record);

                ids.ToList().ForEach(x => users.Add(new ReportUsers { ReportId = record.Id, UserId = x }));

                users.ForEach(x => _context.Add(x));

                _context.SaveChanges();

                scope.Complete();
            }
        }

        public IEnumerable<ReportsView> GetAll(Pager pager)
        {
            var result = _context.Reports.AsEnumerable().Select(x => new ReportsView
            {
                Id = x.Id,
                Date = x.Date,
                SubjectId = x.SubjectId,
                Comment = x.Comment,
                Mark = x.Mark,
                FileId = x.FileId,
                FileName = x.FileName
            });

            pager.TotalItems = result.Count();

            result = result
                    .Skip(pager.PageSize * (pager.CurrentPage - 1))
                    .Take(pager.PageSize);

            if (pager.SortDirection == "ASC")
            {
                result = result.OrderBy(x => x.GetType().GetProperty(pager.SortExpression).GetValue(x));
            }
            else
            {
                result = result.OrderByDescending(x => x.GetType().GetProperty(pager.SortExpression).GetValue(x));
            }

            return result.ToList();
        }

        public Reports GetById(int id)
        {
            return _context.Reports.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Reports record)
        {
            _context.Update(record);
            _context.SaveChanges();
        }
    }
}
