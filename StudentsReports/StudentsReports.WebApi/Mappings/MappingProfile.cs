using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudentsReports.Domain.Models;
using StudentsReports.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsReports.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.UsersView, WebApi.Models.UsersView.Users>();
            CreateMap<WebApi.Models.CreateUser, Domain.Models.Users>();
            CreateMap<WebApi.Models.UpdateUser, Domain.Models.Users>();
            CreateMap<Domain.Models.UserDetails, WebApi.Models.UserDetails>();

            CreateMap<WebApi.Helpers.Pager, Domain.Helpers.Pager>();

            CreateMap<WebApi.Models.Course, Domain.Models.Courses>();
            CreateMap<Domain.Models.Courses, WebApi.Models.CoursesView>();

            CreateMap<WebApi.Models.TeacherCourse, Domain.Models.TeacherCourses>();
            CreateMap<Domain.Models.TeacherCoursesView, WebApi.Models.TeacherCoursesView.TeacherCourses>();

            CreateMap<Domain.Models.ReportsView, WebApi.Models.ReportsView.Reports>()
                .ForMember(x => x.FilePath, opts => opts.MapFrom(src => string.Concat("Files", "/", src.FileId, "/", src.FileName)));
        }
    }
}
