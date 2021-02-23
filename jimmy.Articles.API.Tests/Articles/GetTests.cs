using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using jimmy.Articles.API.Domain.Articles.Commands;
using jimmy.Articles.API.Domain.Articles.Queries;
using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace jimmy.Articles.API.Tests.Articles
{
    [Collection(nameof(SliceFixture))]
    public class GetTests
    {
        private readonly SliceFixture _fixture;

        public GetTests(SliceFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_query_for_article()
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "New Article title",
                Body = "New Article body",
                UpdatingDate = DateTime.Now,
                CreationDate = DateTime.Now
            };

            await _fixture.InsertAsync(article);
            var result = await _fixture.SendAsync(new GetArticleByIdQuery(article.Id));

            result.ShouldNotBeNull();
            result.Id.ShouldBe(article.Id);
            result.Title.ShouldBe(article.Title);
            result.Body.ShouldBe(article.Body);
            result.CreationDate.ShouldBe(article.CreationDate);
            result.UpdatingDate.ShouldBe(article.UpdatingDate);
        }

        [Fact]
        public async Task Should_limit_query_for_articles()
        {
            const int limit = 10;
            const bool descending = false;
            foreach (var index in Enumerable.Range(1, 20))
            {
                await _fixture.InsertAsync(new Article
                {
                    Id = Guid.NewGuid(),
                    Title = $"New Article title {index}",
                    Body = $"New Article body {index}",
                    CreationDate = DateTime.Now,
                    UpdatingDate = DateTime.Now
                });
            }

            var result = await _fixture.SendAsync(new GetArticlesQuery(limit, descending));
            result.Count.ShouldBe(limit);
        }
    }
}