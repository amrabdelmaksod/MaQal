using MaQaL.DTOs;
using MaQaL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaQaL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public  async Task<IActionResult> GetAllAsync()
        {
            var articles = await _context.Articles
                .Include(a=>a.Category)
                .Select(m=> new ArticleDetailsDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Text = m.Text,
                    AuthorName = m.AuthorName,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category.Name
                } )
                .OrderBy(m=>m.Title)
                .ToListAsync();
            return Ok(articles);
        }
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetByIdAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            return Ok(article);
        }
        [HttpGet("SearchByTitle")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var article =  _context.Articles.Where(m=>m.Title
            .Contains(title)|| m.Title.StartsWith(title)||m.Title.EndsWith(title))
                .FirstOrDefault();
            if(article == null)
            {
                return BadRequest($"Sorry The Article with Title {title} does not exist");
            }
            return Ok(article);
        }

        [HttpGet("GetByAuthorName")]
        public IActionResult GetByAuthorName(string authorName)
        {
            var articles = _context.Articles.Where(m=>m.AuthorName == authorName).ToList();
            if(articles.Count == 0)
            {
                return BadRequest($"There is no authors with name {authorName}");
            }
            return Ok(articles);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte categoryId)
        {
            var articlesInCategory =
                 _context.Articles
                .Where(a => a.CategoryId == categoryId)
                .OrderBy(a => a.Title)
                .Include(a => a.Category);

            return Ok(articlesInCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ArticleDTO dto)
        {
            var article = new Article { 
                
                Title = dto.Title,
                Text = dto.Text,
                AuthorName=dto.AuthorName,
                CategoryId = dto.CategoryId
            };
            await _context.Articles.AddAsync(article);
             _context.SaveChanges();
            return Ok(article);
        }

       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateAsync(int id, [FromForm] ArticleDTO dto) 
        {
            var articleInDb = await _context.Articles.FindAsync(id);
            if(articleInDb == null)
            {
                return BadRequest($"The Article with {id} does not exist");
            }
            articleInDb.Title = dto.Title;
            articleInDb.Text = dto.Text;
            articleInDb.AuthorName = dto.AuthorName;
            articleInDb.CategoryId = dto.CategoryId;
            await _context.SaveChangesAsync();
            return Ok("The Article Updated successfully");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var articleInDb =await _context.Articles.FindAsync(id);
            if(articleInDb == null)
            {
                return BadRequest($"The Article with id : {id} Does Not Exists");
            }

            _context.Articles.Remove(articleInDb);
            await _context.SaveChangesAsync();
            return Ok("The Article Deleted Successfully!");
        }
    }
}
