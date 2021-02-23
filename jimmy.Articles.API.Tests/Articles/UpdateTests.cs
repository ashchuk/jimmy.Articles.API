using System;
using System.Threading.Tasks;
using jimmy.Articles.API.Domain.Articles.Commands;
using jimmy.Articles.API.Models;
using Shouldly;
using Xunit;

namespace jimmy.Articles.API.Tests.Articles
{
    [Collection(nameof(SliceFixture))]
    public class UpdateTests
    {
        private readonly SliceFixture _fixture;

        public UpdateTests(SliceFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_update_article()
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = "New Article title",
                Body = "New Article body",
                UpdatingDate = DateTime.Now,
                CreationDate = DateTime.Now
            };

            UpdateArticleCommand command = new UpdateArticleCommand(article.Id,
                title: "Updated title",
                body: "Updated body");

            await _fixture.InsertAsync(article);
            await _fixture.SendAsync(command);

            var edited = await _fixture.FindAsync<Article>(article.Id);
            
            edited.ShouldNotBeNull();
            edited.Id.ShouldBe(article.Id);
            edited.Title.ShouldBe(command.Title);
            edited.Body.ShouldBe(command.Body);
            edited.CreationDate.ShouldBe(article.CreationDate);
            edited.UpdatingDate.ShouldBeGreaterThan(article.UpdatingDate);
        }
    }
}