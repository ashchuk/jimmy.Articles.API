using jimmy.Articles.API.Models.Auth;
using MediatR;

namespace jimmy.Articles.API.Domain.Auth.Commands
{
    public class AuthUserCommand: IRequest<User>
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public AuthUserCommand(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}