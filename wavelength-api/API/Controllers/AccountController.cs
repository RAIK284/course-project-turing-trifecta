using System.Security.Claims;
using API.Services;
using Application.Account;
using Application.DataTransferObject;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<UserDTO>> Login(Login.Params param)
    {
        var user = await Mediator.Send(new Login.Command(param));

        if (user != null) return user;

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(Register.Params param)
    {
        var user = await Mediator.Send(new Register.Command(param));

        if (user != null) return user;

        return Unauthorized();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await Mediator.Send(new CurrentUser.Query(new CurrentUser.Params
        {
            Email = User.FindFirstValue(ClaimTypes.Email)
        }));

        if (user != null) return user;

        return Unauthorized();
    }
}