using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;

namespace GradeBook.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SubjectViewModel, SubjectDto>();
            CreateMap<TeacherViewModel, TeacherDto>();

            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Account, AccountDto>()
                .ForMember(m => m.Email, t => t.MapFrom(s => s.Login));
            CreateMap<Teacher, TeacherDto>()
                .ForMember(m => m.Role, t => t.MapFrom(s => s.Account.Role))
                .ForMember(m => m.FirstName, t => t.MapFrom(s => s.Account.FirstName))
                .ForMember(m => m.LastName, t => t.MapFrom(s => s.Account.LastName))
                .ForMember(m => m.MiddleName, t => t.MapFrom(s => s.Account.MiddleName));
        }
    }
}