using MaQaL.DTOs;
using MaQaL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaQaL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDTO dto)
        {
            var category = new Category { Name = dto.Name };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CategoryDTO dto)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if(categoryInDb == null)
            {
                return BadRequest($"The Category With {id} does not exist");
            }
            categoryInDb.Name = dto.Name;

           await _context.SaveChangesAsync();
            return Ok(categoryInDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var categoryInDb = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if(categoryInDb == null)
            {
                return BadRequest($"The Category with {id} does not exist");
            }
            _context.Categories.Remove(categoryInDb);
            await _context.SaveChangesAsync();
            return (Ok("The Category Deleted Successfully"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var categoryInDb = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return Ok(categoryInDb);
        }
    }
}
