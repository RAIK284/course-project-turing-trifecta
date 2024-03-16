using Application.HubServices;
using Domain;
using Microsoft.AspNetCore.SignalR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace API.SignalR.HubServices;

public class GameRoundHubService : IGameRoundHubService
{
    private readonly IHubContext<GameSessionHub> gameSessionHub;
    private readonly IGameRoundRepository gameRoundRepository;
    private readonly IGameSessionMemberRepository gameSessionMemberRepository;

    public GameRoundHubService(
        IHubContext<GameSessionHub> gameSessionHub, 
        IGameRoundRepository gameRoundRepository,
        IGameSessionMemberRepository gameSessionMemberRepository)
    {
        this.gameSessionHub = gameSessionHub;
        this.gameRoundRepository = gameRoundRepository;
        this.gameSessionMemberRepository = gameSessionMemberRepository;
    }

    public async Task NotifyRoundStart(Guid gameSessionID, GameRoundDTO gameRound)
    {
        var targetOffset = gameRound.TargetOffset;
        foreach (var roundRole in gameRound.RoundRoles)
        {
            // Only show the psychic the target offset.
            gameRound.TargetOffset = roundRole.Role == TeamRole.PSYCHIC ? targetOffset : -1;
                
            await gameSessionHub.Clients
                .Group(GameSessionHub.GroupNameForIndividual(roundRole.UserID, gameSessionID))
                .SendAsync("RoundStarted", gameRound);
        }
    }

    public async Task NotifyTeamTurnGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("TeamTurnGhostGuess", guess);
    }

    public async Task NotifyTeamTurnSelectorSelect(Guid gameSessionID, GameRoundSelectorSelectionDTO selection)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("TeamTurnSelectorSelect", selection);
    }

    public async Task NotifyPsychicClue(Guid gameSessionID, GameRoundDTO gameRoundWithClue)
    {
        var targetOffset = gameRoundWithClue.TargetOffset;
        foreach (var roundRole in gameRoundWithClue.RoundRoles)
        {
            // Only show the psychic the target offset.
            gameRoundWithClue.TargetOffset = roundRole.Role == TeamRole.PSYCHIC ? targetOffset : -1;
                
            await gameSessionHub.Clients
                .Group(GameSessionHub.GroupNameForIndividual(roundRole.UserID, gameSessionID))
                .SendAsync("PsychicGaveClue", gameRoundWithClue);
        }
    }

    public async Task NotifyOpposingTeamGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("OpposingTeamGhostGuess", guess);
    }

    public async Task NotifyOpposingTeamSelectorSelect(Guid gameSessionID, GameRoundOpposingTeamSelectionDTO selection)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("OpposingTeamSelectorSelect", selection);
    }
}