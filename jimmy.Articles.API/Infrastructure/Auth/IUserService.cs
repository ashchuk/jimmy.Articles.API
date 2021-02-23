using System.Threading.Tasks;
using jimmy.Articles.API.Models.Auth;

namespace jimmy.Articles.API.Infrastructure.Auth
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}