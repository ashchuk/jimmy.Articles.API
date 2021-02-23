using System.ComponentModel.DataAnnotations;

namespace jimmy.Articles.API.Models.Auth
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}