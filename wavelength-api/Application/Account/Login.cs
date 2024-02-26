using System.ComponentModel.DataAnnotations;
using API.Services;
using Application.DataTransferObject;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account;

public class Login
{
    public class Params
    {
        [Required] public string Email { get; init; }

        [Required] public string Password { get; init; }
    }

    public class Command : IRequest<UserDTO?>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, UserDTO?>
    {
        private readonly TokenService tokenService;
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDTO?> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Param.Email);

            if (user == null) return null;

            var result = await userManager.CheckPasswordAsync(user, request.Param.Password);

            if (!result) return null;

            return new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                AvatarID = user.AvatarID,
                ID = Guid.Parse(user.Id)
            };
        }
    }
}