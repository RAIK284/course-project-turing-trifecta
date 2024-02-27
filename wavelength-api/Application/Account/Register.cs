using System.ComponentModel.DataAnnotations;
using API.Services;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Application.Account;

public class Register
{
    public class Params
    {
        [Required] public string UserName { get; init; } = string.Empty;

        [Required] [EmailAddress] public string Email { get; init; } = string.Empty;

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*).{8,}$",
            ErrorMessage = "Password does not meet requirements.")]
        public string Password { get; init; }

        [Required] public Guid AvatarID { get; init; }
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
            var doesUsernameExist = await userManager.Users.AnyAsync(u => u.UserName == request.Param.UserName);
            var doesEmailExist = await userManager.Users.AnyAsync(u => u.Email == request.Param.Email);

            if (doesUsernameExist || doesEmailExist) return null;

            var user = new User
            {
                Email = request.Param.Email,
                UserName = request.Param.UserName,
                AvatarID = request.Param.AvatarID
            };

            var result = await userManager.CreateAsync(user, request.Param.Password);

            if (result.Succeeded)
                return new UserDTO
                {
                    Email = user.Email,
                    Token = tokenService.CreateToken(user),
                    UserName = user.UserName,
                    AvatarID = user.AvatarID
                };

            return null;
        }
    }
}