using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.Models
{
    public class UsersView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
