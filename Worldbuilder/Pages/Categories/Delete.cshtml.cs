using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public DeleteModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FindAsync(id);

            if (Category != null)
            {


                foreach (var categoryType in _context.CategoryTypes.Include(x => x.Categories))
                {
                    if (categoryType.Categories.Contains(this.Category))
                        categoryType.Categories.Remove(this.Category);
                }


                foreach (var brickCategory in _context.BrickCategories)
                {
                    if (brickCategory.CategoryId.Equals(this.Category.Id))
                    {
                        _context.BrickCategories.Remove(brickCategory);
                    }
                }

                _context.Categories.Remove(Category);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
