using System;

namespace jimmy.Articles.API.Models
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}