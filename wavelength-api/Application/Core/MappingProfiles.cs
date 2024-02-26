using System.Diagnostics;
using Application.DataTransferObject;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, Activity>();
        CreateMap<User, UserDTO>();
    }
}