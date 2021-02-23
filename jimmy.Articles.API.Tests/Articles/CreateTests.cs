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
    public class CreateTests
    {
        private readonly SliceFixture _fixture;

        public CreateTests(SliceFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_create_new_article()
        {
            Article article = null;
            CreateArticleCommand command = null;

            await _fixture.ExecuteDbContextAsync(async (context, mediator) =>
            {
                command = new CreateArticleCommand(title: "Test title", body: "Test body");
                article = await mediator.Send(command);
            });

            var created = await _fixture
                .ExecuteDbContextAsync(db => db.Articles.Where(c => c.Id == article.Id).SingleOrDefaultAsync());

            created.ShouldNotBeNull();
            created.Body.ShouldBe(command.Body);
            created.Title.ShouldBe(command.Title);
        }
    }
}