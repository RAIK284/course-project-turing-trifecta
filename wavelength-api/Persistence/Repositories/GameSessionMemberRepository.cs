using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class GameSessionMemberRepository : IGameSessionMemberRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public GameSessionMemberRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<GameSessionMemberDTO?> JoinTeam(Guid userID, Guid gameSessionID, Team team)
    {
        var existingMember = await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
            .Where(gs => gs.UserID == userID.ToString())
            .FirstOrDefaultAsync();

        // There is no existing member-- something isn't right
        if (existingMember == null) return null;

        existingMember.Team = team;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameSessionMemberDTO>(existingMember)
            : null;
    }

    public async Task<GameSessionMemberDTO?> Get(Guid userID, Guid gameSessionID)
    {
        return await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
            .Where(gs => gs.UserID == userID.ToString())
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}