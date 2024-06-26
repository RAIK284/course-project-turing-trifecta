﻿using Application.HubServices;
using Domain;
using Microsoft.AspNetCore.SignalR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace API.SignalR.HubServices;

public class GameRoundHubService : IGameRoundHubService
{
    private readonly IGameRoundRepository gameRoundRepository;
    private readonly IHubContext<GameSessionHub> gameSessionHub;
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

    public async Task NotifyRoundStart(Guid gameSessionId, GameSessionDTO gameSession)
    {
        var gameRound = gameSession.Rounds[gameSession.GameRound];
        var targetOffset = gameRound.TargetOffset;
        foreach (var roundRole in gameRound.RoundRoles)
        {
            // Only show the psychic the target offset.
            gameRound.TargetOffset = roundRole.Role == TeamRole.PSYCHIC ? targetOffset : -1;

            await gameSessionHub.Clients
                .Group(GameSessionHub.GroupNameForIndividual(roundRole.UserId, gameSessionId))
                .SendAsync("RoundStarted", gameSession);
        }
    }

    public async Task NotifyTeamTurnGhostGuess(Guid gameSessionId, GameRoundGhostGuessDTO guess)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("TeamTurnGhostGuess", guess);
    }

    public async Task NotifyTeamTurnSelectorSelect(Guid gameSessionId, GameRoundSelectorSelectionDTO selection)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("TeamTurnSelectorSelect", selection);
    }

    public async Task NotifyRoundEnd(Guid gameSessionId, GameSessionDTO gameSession)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("RoundEnded", gameSession);
    }

    public async Task NotifyPsychicClue(Guid gameSessionId, GameRoundDTO gameRoundWithClue)
    {
        var targetOffset = gameRoundWithClue.TargetOffset;
        foreach (var roundRole in gameRoundWithClue.RoundRoles)
        {
            // Only show the psychic the target offset.
            gameRoundWithClue.TargetOffset = roundRole.Role == TeamRole.PSYCHIC ? targetOffset : -1;

            await gameSessionHub.Clients
                .Group(GameSessionHub.GroupNameForIndividual(roundRole.UserId, gameSessionId))
                .SendAsync("PsychicGaveClue", gameRoundWithClue);
        }
    }

    public async Task NotifyOpposingTeamGhostGuess(Guid gameSessionId,
        GameRoundOpposingTeamGuessDTO guess)
    {
        await gameSessionHub.Clients
            .Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("OpposingTeamGhostGuess", guess);
    }
    
    
}