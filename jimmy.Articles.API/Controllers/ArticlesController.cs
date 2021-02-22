using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jimmy.Articles.API.Controllers
{
    public class ArticlesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(int limit, bool descending)
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            return Ok();
        }
    }
}