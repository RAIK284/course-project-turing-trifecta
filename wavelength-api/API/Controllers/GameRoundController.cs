using System.Security.Claims;
using Application.GameRound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GameRoundController : BaseAPIController
{
    [Authorize(Policy = "IsPsychicOnTeamTurn")]
    [HttpPost("giveClue")]
    public async Task<ActionResult> PsychicGiveClue([FromBody] GiveClue.Params param)
    {
        return HandleResult(await Mediator.Send(new GiveClue.Command(param)));
    }

    // perform the ghost guess
    [Authorize(Policy = "IsGhostOnTeamTurn")]
    [HttpPost("performGhostGuess")]
    public async Task<ActionResult> PerformGhostGuess([FromBody] PerformGhostGuess.Param param)
    {
        param.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new PerformGhostGuess.Command(param)));
    }

    // select the target for the 'selector' role
    [Authorize(Policy = "IsSelectorOnTeamTurn")]
    [HttpPost("selectTarget")]
    public async Task<ActionResult> SelectTarget([FromBody] SelectTarget.Param param)
    {
        param.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new SelectTarget.Command(param)));
    }

    // Perform opposing team guess
    [Authorize(Policy = "IsGhostOnOpposingTeam")]
    [HttpPost("performOpposingTeamGuess")]
    public async Task<ActionResult> PerformOpposingTeamGuess([FromBody] PerformOpposingTeamGuess.Param param)
    {
        param.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new PerformOpposingTeamGuess.Command(param)));
    }

    // Perform opposing team selection
    [Authorize(Policy = "IsSelectorOnOpposingTeam")]
    [HttpPost("performOpposingTeamSelection")]
    public async Task<ActionResult> PerformOpposingTeamSelection([FromBody] PerformOpposingTeamSelection.Param param)
    {
        param.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new PerformOpposingTeamSelection.Command(param)));
    }
}