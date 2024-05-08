using System.Security.Claims;
using API.Services;
using Application.Account;
using Application.GameRound;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataTransferObject;

namespace API.Controllers;

public class AccountController : BaseAPIController
{
    private readonly TokenService tokenService;
    private readonly UserManager<User> userManager;

    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login(Login.Params param)
    {
        return HandleResult(await Mediator.Send(new Login.Command(param)));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(Register.Params param)
    {
        return HandleResult(await Mediator.Send(new Register.Command(param)));
    }
    
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        return HandleResult(await Mediator.Send(new CurrentUser.Query(new CurrentUser.Params
        {
            Email = User.FindFirstValue(ClaimTypes.Email)
        })));
    }

    [Authorize]
    [HttpPost("updateUserProfile")]
    public async Task<ActionResult<UserDTO>> UpdateUserProfile([FromBody] UpdateProfile.Params param)
    {
        return HandleResult(await Mediator.Send(new UpdateProfile.Command(param)));
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<ActionResult<UserDTO>> UpdateUserPassword([FromBody] ChangePassword.Params param)

    {   
        param.UserID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await Mediator.Send(new ChangePassword.Command(param));
        return HandleResult(await Mediator.Send(new CurrentUser.Query(new CurrentUser.Params
        {
            Email = User.FindFirstValue(ClaimTypes.Email)
        })));
    }
}