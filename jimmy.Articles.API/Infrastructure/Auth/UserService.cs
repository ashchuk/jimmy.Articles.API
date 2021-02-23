using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jimmy.Articles.API.Infrastructure.Extensions;
using jimmy.Articles.API.Models.Auth;

namespace jimmy.Articles.API.Infrastructure.Auth
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "test", Password = "test" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task
                .Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            return user?.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}