using Application.Contracts.DTOs.Administrator;
using Application.Contracts.DTOs.Parent;
using Application.Contracts.DTOs.Student;
using Application.Contracts.DTOs.Teacher;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTeacherDto, Teacher>()
            .ConstructUsing(dto => new Teacher(
                dto.Name,
                dto.Surname,
                dto.Login,
                dto.Password,
                dto.PhoneNumber,
                dto.BankName,
                dto.CardNumber,
                dto.DateCreated,
                dto.IsDeleted
            ));

        CreateMap<ChangeTeacherDto, Teacher>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<ChangeAdministratorDto, Administrator>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<CreateParentDto, Parent>()
            .ConstructUsing(dto => new Parent(
                dto.Name,
                dto.Surname,
                dto.Login,
                dto.Password,
                dto.PhoneNumber,
                DateTime.UtcNow
            ));

        CreateMap<ChangeParentDto, Parent>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<CreateStudentDto, Student>()
            .ConstructUsing(dto => new Student(
                dto.Number,
                dto.Name,
                dto.Surname,
                dto.Class,
                DateTime.UtcNow,
                dto.Comment,
                dto.TeacherName
            ));

        CreateMap<ChangeStudentDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        ShouldMapProperty = p => p.GetMethod != null && p.SetMethod != null;
    }
}