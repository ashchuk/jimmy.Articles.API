using System;
using System.ComponentModel.DataAnnotations;

namespace jimmy.Articles.API.Models
{
    public class Article: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Body { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime UpdatingDate { get; set; }
    }
}