using ExtremeSports2.Data;
using ExtremeSports2.Data.Entities;
using ExtremeSports2.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtremeSports2.Controllers
{
    public class CategoriesController: Controller
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'DataContext.Countries'  is null.");
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Category());
            }
            else
            {
                Category category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();

                }
                return Json(new
                {
                    isValid = true,
                    html = ModalHelper.RenderRazorViewToString(
                          this,
                          "_ViewAll",
                          _context.Categories
                              .ToList())
                });

            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", category) });

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction(nameof(Index));
        }
    }


}
