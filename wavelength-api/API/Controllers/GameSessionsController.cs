using Application.GameSessions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class GameSessionsController : BaseAPIController
{
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GameSession>> GetGameSession(Guid id)
    {
        return await Mediator.Send(new Get.Query(new Get.Params
        {
            ID = id
        }));
    }
    
    [HttpPost]
    public async Task<ActionResult<GameSession>> CreateGameSession(Create.Params param)
    {
        return await Mediator.Send(new Create.Command(param));
    }
}