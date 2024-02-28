using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GameRoundController : BaseAPIController
{
    [Authorize(Policy = "IsGhostOnTeamTurn")]
    [HttpPost("performGhostGuess")]
    public async Task<ActionResult> PerformGhostGuess()
    {
        return null;
    }
}