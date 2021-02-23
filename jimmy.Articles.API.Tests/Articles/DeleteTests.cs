using System;
using System.Linq;
using System.Threading.Tasks;
using jimmy.Articles.API.Domain.Articles.Commands;
using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace jimmy.Articles.API.Tests.Articles
{
 [Collection(nameof(SliceFixture))]
    public class DeleteTests
    {
        private readonly SliceFixture _fixture;

        public DeleteTests(SliceFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_query_for_command()
        {
            var testTime = DateTime.Now;
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "New Article title",
                Body = "New Article body",
            };

            await _fixture.InsertAsync(article);
            var result = await _fixture.SendAsync(new DeleteArticleCommand(article.Id));

            result.ShouldNotBeNull();
            result.Id.ShouldBe(article.Id);
            result.Title.ShouldBe(article.Title);
            result.Body.ShouldBe(article.Body);
        }

        [Fact]
        public async Task Should_delete()
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "New Article title",
                Body = "New Article body",
                CreationDate = DateTime.Now,
                UpdatingDate = DateTime.Now
            };

            await _fixture.InsertAsync(article);
            await _fixture.SendAsync(new DeleteArticleCommand(article.Id));
            var result = await _fixture.ExecuteDbContextAsync(db => db.Articles.Where(c => c.Id == article.Id).SingleOrDefaultAsync());
            result.ShouldBeNull();
        }
    }
}