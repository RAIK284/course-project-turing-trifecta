using System.Security.Claims;
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

    [HttpPost("create")]
    public async Task<ActionResult> CreateGameSession(Create.Params param)
    {
        param.OwnerID = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new Create.Command(param)));
    }

    [Authorize(Policy = "IsGameSessionOwner")]
    [HttpPost("start")]
    public async Task<ActionResult> StartGameSession(Start.Params param)
    {
        return HandleResult(await Mediator.Send(new Start.Command(param)));
    }

    [Authorize(Policy = "IsNotGameSessionMember")]
    [HttpPost("join")]
    public async Task<ActionResult> JoinGameSession(Join.Params param)
    {
        param.UserID = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return HandleResult(await Mediator.Send(new Join.Command(param)));
    }
}