using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Helpers
{
    public static class ResponseMessage
    {
        public const string IncorrectPassword = "Incorrect password";
        public const string UserAlreadyExist = "The user already exist";
        public const string RoleNotExist = "The role does not exist";
    }
}
