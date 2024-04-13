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

    /// <inheritdoc />
    public async Task<GameSessionMemberDTO?> JoinTeam(Guid userId, Guid gameSessionId, Team team)
    {
        var existingMember = await context.GameSessionMembers
            .Where(gs => gs.GameSessionId == gameSessionId)
            .Where(gs => gs.UserId == userId)
            .Include(gs => gs.User)
            .FirstOrDefaultAsync();

        // There is no existing member-- something isn't right
        if (existingMember == null) return null;

        existingMember.Team = team;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameSessionMemberDTO>(existingMember)
            : null;
    }

    /// <inheritdoc />
    public async Task<List<GameSessionMemberDTO>> GetAll(Guid gameSessionId)
    {
        return await context.GameSessionMembers
            .Where(gs => gs.GameSessionId == gameSessionId)
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<GameSessionMemberDTO>> AssignTeamlessPlayersToTeam(Guid gameSessionId)
    {
        var members = await context.GameSessionMembers
            .Where(gs => gs.GameSessionId == gameSessionId)
            .ToListAsync();
        var teamlessPlayers = members
            .Where(m => m.Team == Team.NONE)
            .ToList();

        foreach (var member in teamlessPlayers)
        {
            var numTeamOnePlayers = members.Count(m => m.Team == Team.ONE);
            var numTeamTwoPlayers = members.Count(m => m.Team == Team.TWO);

            if (numTeamOnePlayers > numTeamTwoPlayers)
                member.Team = Team.TWO;
            else
                member.Team = Team.ONE;
        }

        await context.SaveChangesAsync();

        return mapper.Map<List<GameSessionMemberDTO>>(members);
    }

    /// <inheritdoc />
    public async Task<GameSessionMemberDTO?> Get(Guid userId, Guid gameSessionId)
    {
        return await context.GameSessionMembers
            .Where(gs => gs.GameSessionId == gameSessionId)
            .Where(gs => gs.UserId == userId)
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}