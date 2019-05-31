using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Discriminator { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
