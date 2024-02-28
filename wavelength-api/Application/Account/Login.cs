using System.ComponentModel.DataAnnotations;
using API.Services;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DataTransferObject;

namespace Application.Account;

public class Login
{
    public class Params
    {
        [Required] public string Email { get; init; }

        [Required] public string Password { get; init; }
    }

    public class Command : IRequest<Result<UserDTO>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<UserDTO>>
    {
        private readonly TokenService tokenService;
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<Result<UserDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Param.Email);

            if (user == null) return Result<UserDTO>.Failure("A user with that email does not exist.");

            var result = await userManager.CheckPasswordAsync(user, request.Param.Password);

            if (!result) return Result<UserDTO>.Failure("Incorrect password.");

            return Result<UserDTO>.Success(new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                AvatarID = user.AvatarID,
                ID = Guid.Parse(user.Id)
            });
        }
    }
}