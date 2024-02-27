using AutoMapper;
using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Core;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        _ = CreateMap<User, UserDTO>()
            .ForMember(dto => dto.ID,
                opts => opts.MapFrom(u => Guid.Parse(u.Id)));
    }
}