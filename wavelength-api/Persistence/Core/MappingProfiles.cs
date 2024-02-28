using AutoMapper;
using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        _ = CreateMap<User, UserDTO>()
            .ForMember(dto => dto.ID,
                opts => opts.MapFrom(u => Guid.Parse(u.Id)));
        _ = CreateMap<GameSession, GameSessionDTO>();
        // .PreserveReferences();
        // .ForMember(dest => dest.Members, opt => opt.Ignore());
        _ = CreateMap<GameSessionMember, GameSessionMemberDTO>();
        // .PreserveReferences();
        // .ForMember(dest => dest.GameSession, opt => opt.Ignore());
    }
}