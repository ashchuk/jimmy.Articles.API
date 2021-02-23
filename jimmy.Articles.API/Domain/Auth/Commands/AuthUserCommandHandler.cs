using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Infrastructure.Auth;
using jimmy.Articles.API.Models.Auth;
using MediatR;

namespace jimmy.Articles.API.Domain.Auth.Commands
{
    public class AuthUserCommandHandler: IRequestHandler<AuthUserCommand, User>
    {
        private readonly IUserService _userService;

        public AuthUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            return user;
        }
    }
}