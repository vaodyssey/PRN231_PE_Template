using AutoMapper;
using Repository.Entities;
using Repository.Payload.Request.StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewStudentRequest, Student>()
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateStudentRequest, Student>()
              .ForMember(dest => dest.Group, opt => opt.Ignore())
              .ReverseMap();
        }
    }
}