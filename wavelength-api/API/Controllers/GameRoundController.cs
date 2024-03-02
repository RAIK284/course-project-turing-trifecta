using Application.GameRound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataTransferObject;

namespace API.Controllers;

public class GameRoundController : BaseAPIController
{
    // perform the ghost guess
    [Authorize(Policy = "IsGhostOnTeamTurn")]
    [HttpPost("performGhostGuess")]
    public async Task<ActionResult> PerformGhostGuess([FromBody] GameRoundGhostGuessDTO param)
    {
        return HandleResult(await Mediator.Send(new PerformGhostGuess.Command(param)));
    }

    // select the target for the 'selector' role
    [Authorize(Policy = "IsSelectorOnTeamTurn")]
    [HttpPost("selectTarget")]
    public async Task<ActionResult> SelectTarget([FromBody] GameRoundSelectorSelectionDTO param)
    {
        return HandleResult(await Mediator.Send(new SelectTarget.Command(param)));
    }

    // Perform opposing team guess
    [Authorize(Policy = "IsGhostOnOpposingTeam")]
    [HttpPost("performOpposingTeamGuess")]
    public async Task<ActionResult> PerformOpposingTeamGuess([FromBody] GameRoundOpposingTeamGuessDTO param)
    {
        return HandleResult(await Mediator.Send(new PerformOpposingTeamGuess.Command(param)));
    }

    // Perform opposing team selection
    [Authorize(Policy = "IsSelectorOnOpposingTeam")]
    [HttpPost("performOpposingTeamSelection")]
    public async Task<ActionResult> PerformOpposingTeamSelection([FromBody] GameRoundOpposingTeamSelectionDTO param)
    {
        return HandleResult(await Mediator.Send(new PerformOpposingTeamSelection.Command(param)));
    }
}