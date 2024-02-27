using System.Diagnostics;
using AutoMapper;
using Domain;
using Persistence.DataTransferObject;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, Activity>();
        CreateMap<User, UserDTO>();
    }
}