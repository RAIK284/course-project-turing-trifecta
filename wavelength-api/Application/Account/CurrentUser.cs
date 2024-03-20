using System.ComponentModel.DataAnnotations;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.Account;

public class CurrentUser
{
    public class Params
    {
        [Required] public string Email { get; init; }
    }

    public class Query : IRequest<Result<UserDTO>>
    {
        public readonly Params Param;

        public Query(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Query, Result<UserDTO>>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager, IMapper mapper, IGameSessionRepository gameSessionRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<UserDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Param.Email);

            if (user == null) return Result<UserDTO>.Failure("User not found.");

            var userDTO = mapper.Map<UserDTO>(user);

            var activeGameSession = await gameSessionRepository.GetActiveSession(userDTO.ID);

            if (activeGameSession != null)
            {
                userDTO.ActiveGameSession = activeGameSession;
                userDTO.ActiveGameSessionID = activeGameSession.ID;
            }

            return Result<UserDTO>.Success(userDTO);
        }
    }
}