using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Models
{
    public class UsersView
    {
        public int TotalItems { get; set; }
        public IEnumerable<Users> Items { get; set; }

        public class Users
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }
    }
}
