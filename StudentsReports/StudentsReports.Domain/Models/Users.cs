using System;
using System.Collections.Generic;

namespace StudentsReports.Domain.Models
{
    public partial class Users
    {
        public Users()
        {
            ReportUsers = new HashSet<ReportUsers>();
            StudentCourses = new HashSet<StudentCourses>();
            Subjects = new HashSet<Subjects>();
            TeacherCourses = new HashSet<TeacherCourses>();
            UserRoles = new HashSet<UserRoles>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public ICollection<ReportUsers> ReportUsers { get; set; }
        public ICollection<StudentCourses> StudentCourses { get; set; }
        public ICollection<Subjects> Subjects { get; set; }
        public ICollection<TeacherCourses> TeacherCourses { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
