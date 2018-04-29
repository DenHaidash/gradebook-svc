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
            CreateMap<AccountViewModel, TeacherDto>();
            CreateMap<AccountViewModel, StudentDto>();
            CreateMap<NewAccountViewModel, TeacherDto>();
            CreateMap<SpecialtyViewModel, SpecialtyDto>();
            CreateMap<GroupViewModel, GroupDto>()
                .ForPath(m => m.Specialty.Id, t => t.MapFrom(s => s.SpecialtyId));
            CreateMap<NewStudentViewModel, StudentDto>()
                .ForPath(m => m.Group.Id, t => t.MapFrom(s => s.GroupId));
            
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Specialty, SpecialtyDto>().ReverseMap();
            CreateMap<Account, AccountDto>()
                .ForMember(m => m.Email, t => t.MapFrom(s => s.Login));
            CreateMap<Teacher, TeacherDto>()
                .ForMember(m => m.Role, t => t.MapFrom(s => s.Account.Role))
                .ForMember(m => m.Email, t => t.MapFrom(s => s.Account.Login))
                .ForMember(m => m.FirstName, t => t.MapFrom(s => s.Account.FirstName))
                .ForMember(m => m.LastName, t => t.MapFrom(s => s.Account.LastName))
                .ForMember(m => m.MiddleName, t => t.MapFrom(s => s.Account.MiddleName));
            CreateMap<Group, GroupDto>();
            CreateMap<Student, StudentDto>()
                .ForMember(m => m.Role, t => t.MapFrom(s => s.Account.Role))
                .ForMember(m => m.Email, t => t.MapFrom(s => s.Account.Login))
                .ForMember(m => m.FirstName, t => t.MapFrom(s => s.Account.FirstName))
                .ForMember(m => m.LastName, t => t.MapFrom(s => s.Account.LastName))
                .ForMember(m => m.MiddleName, t => t.MapFrom(s => s.Account.MiddleName));
            
            CreateMap<GroupDto, Group>()
                .ForMember(m => m.SpecialityRefId, t => t.MapFrom(s => s.Specialty.Id))
                .ForMember(m => m.Specialty, t => t.Ignore());
            CreateMap<AccountDto, Account>()
                .ForMember(m => m.Login, t => t.MapFrom(s => s.Email));
//            CreateMap<StudentDto, Student>()
//                .ForMember(m => m.GroupRefId, t => t.MapFrom(s => s.Group.Id))
//                .ForMember(m => m.Group, t => t.Ignore());
        }
    }
}