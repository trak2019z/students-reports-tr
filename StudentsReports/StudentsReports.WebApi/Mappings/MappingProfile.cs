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
            CreateMap<Users, User>();
        }
    }
}
