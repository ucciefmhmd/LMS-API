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

            CreateMap<Students, StudentWithExamAndInstrcutorCourses>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.userID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Users.Password))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Users.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Users.Address))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Users.Photo))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Group.Select(g => g.InstructorCourse.Courses.Name).Distinct()))
                .ForMember(dest => dest.CourseIDs, opt => opt.MapFrom(src => src.Group.Select(g => g.InstructorCourse.Courses.Id).Distinct()))
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.StudentExam.Select(se => se.Exam.Name)))
                .ForMember(dest => dest.ExamIDs, opt => opt.MapFrom(src => src.StudentExam.Select(se => se.Exam.Id)))
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.StudentExam.Select(se => se.Result)))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Select(g => g.Name)))
                .ReverseMap();

            CreateMap<Students, StudentCrudDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.userID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Users.Password))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Users.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Users.Address))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Users.Photo))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Group.Select(g => g.InstructorCourse.Courses.Name).Distinct()))
                .ForMember(dest => dest.InstructorIDs, opt => opt.MapFrom(src => src.Group.Select(g => g.InstructorCourse.Instructors.userID).Distinct()))
                .ReverseMap();


            CreateMap<Courses, CoursesDTO>().ReverseMap();
            CreateMap<Courses, CoursesWithNumberOfExamDTO>()
                .ForMember(dest => dest.numOfExam, opt => opt.MapFrom(src => src.Exam.Count()));

            CreateMap<Exam, ExamsWithQuestionsAndCoursesDTO>()
                .ForMember(dest => dest.StudentIDs, opt => opt.MapFrom(src => src.StudentExam.Select(a => a.Students.userID)))
                .ForMember(dest => dest.NumberOfQuestions, opt => opt.MapFrom(src => src.Questions.Count()))
                .ReverseMap();

            CreateMap<Questions, QuestionWithExamNameDTO>()
                 .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
                 .ForMember(dest => dest.ChoosesIDs, opt => opt.MapFrom(src => src.ChooseQuestion.Select(a=>a.Ques_ID)))
                 .ForMember(dest => dest.ChoosesName, opt => opt.MapFrom(src => src.ChooseQuestion.Select(a=>a.Choose)))
                 .ReverseMap();

            CreateMap<Questions, QuestionCrudDTO>()
                .ForMember(dest => dest.ChoosesName, opt => opt.MapFrom(src => src.ChooseQuestion.Select(a=>a.Choose)))
                .ReverseMap();
            
            CreateMap<Users, SubAdminDTO>().ReverseMap();
            
            CreateMap<Events, EventDto>().ReverseMap();
            CreateMap<Users, LoginRequestDTO>().ReverseMap();

        }
    }
}
