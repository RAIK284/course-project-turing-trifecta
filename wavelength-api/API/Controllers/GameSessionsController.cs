using Application.GameSessions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GameSessionsController : BaseAPIController
{
    [HttpGet("{id}")]
    public async Task<ActionResult> GetGameSession(Guid id)
    {
        return HandleResult(await Mediator.Send(new Get.Query(new Get.Params
        {
            ID = id
        })));
    }

    [HttpPost]
    public async Task<ActionResult> CreateGameSession(Create.Params param)
    {
        return HandleResult(await Mediator.Send(new Create.Command(param)));
    }
}