using System.ComponentModel.DataAnnotations;
using API.Services;
using Application.Core;
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

        [Required] public Guid AvatarId { get; init; }
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
            var doesUsernameExist = await userManager.Users.AnyAsync(u => u.UserName == request.Param.UserName);
            var doesEmailExist = await userManager.Users.AnyAsync(u => u.Email == request.Param.Email);

            if (doesUsernameExist) 
                return Result<UserDTO>.Failure("A user with this username already exists.");

            if (doesEmailExist)
                return Result<UserDTO>.Failure("A user with this email already exists.");

            Guid userId = Guid.NewGuid();
            
            var user = new User
            {
                Id = userId.ToString(),
                UserId = userId,
                Email = request.Param.Email,
                UserName = request.Param.UserName,
                AvatarId = request.Param.AvatarId
            };

            var result = await userManager.CreateAsync(user, request.Param.Password);

            if (result.Succeeded)
                return Result<UserDTO>.Success(new UserDTO
                {
                    Email = user.Email,
                    Token = tokenService.CreateToken(user),
                    UserName = user.UserName,
                    AvatarId = user.AvatarId
                });

            return Result<UserDTO>.Failure("Something went wrong when trying to register.");
        }
    }
}