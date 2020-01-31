using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.CategoryTypes
{
    public class DeleteModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public DeleteModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryType CategoryType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryType = await _context.CategoryTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (CategoryType == null)
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

            CategoryType = await _context.CategoryTypes.FindAsync(id);

            if (CategoryType != null)
            {

                var categoriesToClean = _context.Categories.Where(cat => cat.CategoryType.Equals(this.CategoryType)).AsEnumerable();


                foreach (var category in categoriesToClean)
                {
                    category.CategoryType = null;
                }

                _context.CategoryTypes.Remove(CategoryType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
