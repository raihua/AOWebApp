using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModels;
using Microsoft.Data.SqlClient;
using AOWebApp.Helpers;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ItemsController(AmazonOrdersContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string searchText, int? categoryId, string SortOrder, int? pageNumber)
        {
            ItemSearch itemSearch = new ItemSearch();

            itemSearch.PageNumber = pageNumber ?? 1;

            #region CategoriesQuery
            var CategoriesQuery =
                from c in _context.ItemCategories
                where c.ParentCategory == null
                orderby c.CategoryName
                select new
                {
                    c.CategoryId,
                    c.CategoryName
                };

            var Categories = CategoriesQuery.ToListAsync();

            itemSearch.CategoryList = new SelectList(await Categories,
                                    nameof(ItemCategory.CategoryId),
                                    nameof(ItemCategory.CategoryName),
                                    categoryId);
            #endregion

            #region ItemQuery
            itemSearch.SearchText = searchText;

            var amazonOrdersContext = _context.Items
                .Include(i => i.Category)
                .AsQueryable();

            itemSearch.SortOrder = SortOrder;

            switch(SortOrder)
            {
                case "nameDesc":
                    amazonOrdersContext = amazonOrdersContext.OrderByDescending(i => i.ItemName);
                    break;
                case "nameAsc":
                    amazonOrdersContext = amazonOrdersContext.OrderBy(i => i.ItemName);
                    break;
                case "costDesc":
                    amazonOrdersContext = amazonOrdersContext.OrderByDescending(i => i.ItemCost);
                    break;
                default:
                    amazonOrdersContext = amazonOrdersContext.OrderBy(i => i.ItemCost);
                    break;
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                amazonOrdersContext = amazonOrdersContext.Where(i => i.ItemName.Contains(searchText));
            }

            if (categoryId.HasValue)
            {
                amazonOrdersContext = amazonOrdersContext.Where(i => i.Category.ParentCategoryId == categoryId.Value);
            }

            int PageSize = 3;
            itemSearch.Items = await PaginatedList<ItemDetail>.CreateAsync(amazonOrdersContext
                .Select(i => new ItemDetail
                {
                    AverageRating = (i.Reviews != null && i.Reviews.Count > 0) ? i.Reviews.Average(r => r.Rating) : 0,
                    NumberOfReviews = i.Reviews != null ? i.Reviews.Count : 0,
                    TheItem = i
                }).AsNoTracking(), itemSearch.PageNumber ?? 1, PageSize);
            #endregion

            return View(itemSearch);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
