using AutoMapper;
using StudentsReports.Domain.Models;
using StudentsReports.WebApi.Models;
using System;
using System.Collections.Generic;
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
        }
    }
}
