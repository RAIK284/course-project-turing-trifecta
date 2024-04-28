using AutoMapper;
using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        _ = CreateMap<User, UserDTO>()
            .ForMember(dto => dto.Id,
                opts => opts.MapFrom(u => Guid.Parse(u.Id)));
        _ = CreateMap<GameSession, GameSessionDTO>()
            .ForMember(gs => gs.Rounds,
                opt => opt.MapFrom(gs => gs.Rounds))
            .ForMember(gs => gs.Members,
                opt => opt.MapFrom(gs => gs.Members));
        _ = CreateMap<GameSessionMember, GameSessionMemberDTO>()
            .ForMember(gs => gs.User, opt => opt.MapFrom(gs => gs.User));
        _ = CreateMap<GameSessionMemberRoundRole, GameSessionMemberRoundRoleDTO>();
        _ = CreateMap<GameRoundGhostGuess, GameRoundGhostGuessDTO>();
        _ = CreateMap<GameRoundSelectorSelection, GameRoundSelectorSelectionDTO>();
        _ = CreateMap<GameRoundOpposingTeamGuess, GameRoundOpposingTeamGuessDTO>();
        _ = CreateMap<GameRoundOpposingTeamSelection, GameRoundOpposingTeamSelectionDTO>();
        _ = CreateMap<GameRound, GameRoundDTO>()
            .ForMember(round => round.RoundRoles,
                opt => opt.MapFrom(round => round.RoundRoles))
            .ForMember(round => round.GhostGuesses,
                opt => opt.MapFrom(round => round.GhostGuesses))
            .ForMember(round => round.SelectorSelection,
                opt => opt.MapFrom(round => round.SelectorSelection))
            .ForMember(round => round.OpposingGhostGuesses,
                opt => opt.MapFrom(round => round.OpposingGhostGuesses))
            .ForMember(round => round.OpposingTeamSelection,
                opt => opt.MapFrom(round => round.OpposingSelectorSelection));
        _ = CreateMap<SpectrumCard, SpectrumCardDTO>();
    }
}