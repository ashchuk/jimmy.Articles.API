using System.Collections.Generic;
using System.Linq;
using jimmy.Articles.API.Models;
using jimmy.Articles.API.Models.Auth;

namespace jimmy.Articles.API.Infrastructure.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user) {
            user.Password = null;
            return user;
        }
    }
}