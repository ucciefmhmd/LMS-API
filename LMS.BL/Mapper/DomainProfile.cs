using AutoMapper;
using LMS.BL.DTO;
using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Instructors, InstructorsWithCourseNameDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.userID))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.Email))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Users.Password))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Users.Phone))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Users.Address))
             .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Users.Photo))
             .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
             .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.InstructorCourse.Select(ic => ic.Courses.Name).Distinct()))
             .ReverseMap();

            CreateMap<Courses, CoursesDTO>().ReverseMap();
        }
    }
}
