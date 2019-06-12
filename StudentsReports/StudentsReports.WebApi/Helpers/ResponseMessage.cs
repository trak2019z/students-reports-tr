using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Helpers
{
    public static class ResponseMessage
    {
        public const string IncorrectUserNameOrPassword = "Incorrect userName or password";
        public const string UserAlreadyExists = "The user does already exists";
        public const string RoleNotExists = "The role does not exists";
        public const string CourseAlreadyExists = "The course does already exists";
        public const string TeacherCourseAlreadyExists = "The teacher course does already exists";
    }
}
