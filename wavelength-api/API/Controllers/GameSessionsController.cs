using Application.GameSessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GameSessionsController : BaseAPIController
{
    [Authorize(Policy = "IsGameSessionMember")]
    [HttpGet("{gameSessionID}")]
    public async Task<ActionResult> GetGameSession(Guid gameSessionID)
    {
        return HandleResult(await Mediator.Send(new Get.Query(new Get.Params
        {
            GameSessionID = gameSessionID
        })));
    }

    [HttpPost]
    public async Task<ActionResult> CreateGameSession(Create.Params param)
    {
        return HandleResult(await Mediator.Send(new Create.Command(param)));
    }
}