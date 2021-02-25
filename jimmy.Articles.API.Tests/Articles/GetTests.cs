using System;
using System.Linq;
using System.Threading.Tasks;
using jimmy.Articles.API.Domain.Articles.Queries;
using jimmy.Articles.API.Models;
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
            const int offset = 0;
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

            var result = await _fixture.SendAsync(new GetArticlesQuery(limit, offset, descending));
            result.Count.ShouldBe(limit);
        }

        [Fact]
        public async Task Should_be_sorted_by_ascending()
        {
            const int limit = 10;
            const int offset = 20;
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
                await Task.Delay(200);
            }

            var result = await _fixture.SendAsync(new GetArticlesQuery(limit, offset, descending));

            var firstDate = result.First().CreationDate;
            var lastDate = result.Last().CreationDate;
            firstDate.ShouldBeLessThan(lastDate);
        }

        [Fact]
        public async Task Should_be_sorted_by_descending()
        {
            const int limit = 10;
            const int offset = 20;
            const bool descending = true;
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
                await Task.Delay(200);
            }

            var result = await _fixture.SendAsync(new GetArticlesQuery(limit, offset, descending));

            var firstDate = result.First().CreationDate;
            var lastDate = result.Last().CreationDate;
            firstDate.ShouldBeGreaterThan(lastDate);
        }

        [Fact]
        public async Task Should_skip_right_number_of_articles()
        {
            const int limit = 5;
            const int offset = 5;
            const bool descending = false;
            foreach (var index in Enumerable.Range(1, 20))
            {
                await _fixture.InsertAsync(new Article
                {
                    Id = Guid.NewGuid(),
                    Title = $"{index}",
                    Body = $"{index}",
                    CreationDate = DateTime.Now,
                    UpdatingDate = DateTime.Now
                });
                await Task.Delay(200);
            }

            var result = await _fixture.SendAsync(new GetArticlesQuery(limit, offset, descending));
            result.First().Title.ShouldBe((limit + 1).ToString());
        }
    }
}