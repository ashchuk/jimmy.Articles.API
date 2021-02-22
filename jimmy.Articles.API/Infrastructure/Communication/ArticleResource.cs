using System.ComponentModel.DataAnnotations;

namespace jimmy.Articles.API.Infrastructure.Communication
{
    public class ArticleResource
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Body { get; set; }
    }
}