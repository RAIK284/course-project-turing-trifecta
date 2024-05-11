using System.ComponentModel.DataAnnotations;
using Application.Core;
using Application.HubServices;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class ChangePassword
{
    public class Params
    {
        public Guid UserID { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*).{8,}$",
            ErrorMessage = "Password does not meet requirements.")]
        public string Password { get; init; }

    }

    public class Command : IRequest<Result<bool>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUsersRepository usersRepository;


        public Handler(
            UserManager<User> userManager,
            IUsersRepository usersRepository)
        {
            this.userManager = userManager;
            this.usersRepository = usersRepository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
           var user = await userManager.FindByIdAsync(request.Param.UserID.ToString());
           var token = await userManager.GeneratePasswordResetTokenAsync(user);
           var result = await userManager.ResetPasswordAsync(user, token, request.Param.Password);

           if (!result.Succeeded) return Result<bool>.Failure("Password change failed.");

            return Result<bool>.Success(true);
        }
    }
}