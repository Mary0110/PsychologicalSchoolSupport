using AutoMapper;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDTO, User>();
        
        CreateMap<UpdateUserDTO, User>();
    }
}
