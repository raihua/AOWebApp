using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;

namespace AOWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentCategoriesWebAPI : ControllerBase
    {
        private readonly AmazonOrdersContext _context;

        public ParentCategoriesWebAPI(AmazonOrdersContext context)
        {
            _context = context;
        }

        // GET: api/ParentCategoriesWebAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetItemCategories()
        {
            var parentCategories = await _context.ItemCategories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();

            return parentCategories;
        }

        // GET: api/ParentCategoriesWebAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCategory>> GetItemCategory(int id)
        {
            var itemCategory = await _context.ItemCategories.FindAsync(id);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ParentCategoriesWebAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(int id, ItemCategory itemCategory)
        {
            if (id != itemCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(itemCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParentCategoriesWebAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(ItemCategory itemCategory)
        {
            _context.ItemCategories.Add(itemCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCategory", new { id = itemCategory.CategoryId }, itemCategory);
        }

        // DELETE: api/ParentCategoriesWebAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCategory(int id)
        {
            var itemCategory = await _context.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            _context.ItemCategories.Remove(itemCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCategoryExists(int id)
        {
            return _context.ItemCategories.Any(e => e.CategoryId == id);
        }
    }
}
