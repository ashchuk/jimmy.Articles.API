using System;
using System.Threading.Tasks;
using jimmy.Articles.API.Domain.Articles.Commands;
using jimmy.Articles.API.Domain.Articles.Queries;
using jimmy.Articles.API.Infrastructure.Communication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jimmy.Articles.API.Controllers
{    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IMediator _mediator;
        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArticleResource resource)
        {
            return Ok(await _mediator.Send(new CreateArticleCommand(resource.Title, resource.Body)));
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListArticlesResource resource)
        {
            return Ok(await _mediator.Send(new GetArticlesQuery(resource.Limit, resource.Offset, resource.OrderByDescending)));
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetArticleByIdQuery(id)));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteArticleCommand(id)));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ArticleResource resource)
        {
            return Ok(await _mediator.Send(new UpdateArticleCommand(id, resource.Title, resource.Body)));
        }
    }
}