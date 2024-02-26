using System.ComponentModel.DataAnnotations;
using Application.DataTransferObject;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account;

public class CurrentUser
{
    public class Params
    {
        [Required] public string Email { get; init; }
    }

    public class Query : IRequest<UserDTO?>
    {
        public readonly Params Param;

        public Query(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Query, UserDTO?>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Param.Email);

            if (user == null) return null;

            return mapper.Map<UserDTO>(user);
        }
    }
}