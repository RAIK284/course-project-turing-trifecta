using System.ComponentModel.DataAnnotations;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DataTransferObject;

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
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<UserDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Param.Email);

            if (user == null) return Result<UserDTO>.Failure("User not found.");

            return Result<UserDTO>.Success(mapper.Map<UserDTO>(user));
        }
    }
}