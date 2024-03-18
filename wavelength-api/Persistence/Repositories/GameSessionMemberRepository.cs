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
    public async Task<GameSessionMemberDTO?> JoinTeam(Guid userID, Guid gameSessionID, Team team)
    {
        var existingMember = await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
            .Where(gs => gs.UserID == userID)
            .FirstOrDefaultAsync();

        // There is no existing member-- something isn't right
        if (existingMember == null) return null;

        existingMember.Team = team;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameSessionMemberDTO>(existingMember)
            : null;
    }

    /// <inheritdoc />
    public async Task<List<GameSessionMemberDTO>> GetAll(Guid gameSessionID)
    {
        return await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<GameSessionMemberDTO>> AssignTeamlessPlayersToTeam(Guid gameSessionID)
    {
        var members = await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
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
    public async Task<GameSessionMemberDTO?> Get(Guid userID, Guid gameSessionID)
    {
        return await context.GameSessionMembers
            .Where(gs => gs.GameSessionID == gameSessionID)
            .Where(gs => gs.UserID == userID)
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}