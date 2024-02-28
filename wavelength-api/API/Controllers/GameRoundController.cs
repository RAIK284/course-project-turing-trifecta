using Application.GameRound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataTransferObject;

namespace API.Controllers;

public class GameRoundController : BaseAPIController
{
    [Authorize(Policy = "IsGhostOnTeamTurn")]
    [HttpPost("performGhostGuess")]
    public async Task<ActionResult> PerformGhostGuess([FromBody] GameRoundGhostGuessDTO param)
    {
        return HandleResult(await Mediator.Send(new PerformGhostGuess.Command(param)));
    }
}