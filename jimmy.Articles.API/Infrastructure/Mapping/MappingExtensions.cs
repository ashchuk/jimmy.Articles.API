using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jimmy.Articles.API.Infrastructure.Communication;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Infrastructure.Mapping
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();

    }
}