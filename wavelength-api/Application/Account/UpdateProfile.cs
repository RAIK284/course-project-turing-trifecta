using Application.Core;
using Application.HubServices;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class UpdateProfile
{
    public class Params
    {
        public UserDTO User { get; set; }
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
        private readonly IUsersRepository usersRepository;

        public Handler(
            IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<Result<UserDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await usersRepository.UpdateProfile(request.Param.User);

            if (result == null) return Result<UserDTO>.Failure("Profile update failed.");

            return Result<UserDTO>.Success(result);
        }
    }
}