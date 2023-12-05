using AutoMapper;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.API.Profiles;

public class StudentProfile: Profile
{
    public StudentProfile()
    {
        CreateMap<AddStudentDTO, Student>();
        CreateMap<UpdateStudentDTO, Student>();
        CreateMap<Student, UpdateStudentDTO>();
        CreateMap<Student, AddStudentDTO>();
    }
}
