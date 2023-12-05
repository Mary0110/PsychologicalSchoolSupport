using AutoMapper;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.API.Profiles;

public class FormProfile: Profile
{
    public FormProfile()
    {
        CreateMap<AddFormDTO, Form>();
        CreateMap<Form, AddFormDTO>();
    }
}
