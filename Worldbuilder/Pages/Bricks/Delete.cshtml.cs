using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Bricks
{
    public class DeleteModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public DeleteModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Brick Brick { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brick = await _context.Bricks.FirstOrDefaultAsync(m => m.Id == id);

            if (Brick == null)
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

            Brick = await _context.Bricks.FindAsync(id);

            if (Brick != null)
            {


                foreach (var joinTable in _context.BrickToBrick)
                {
                    if (joinTable.BrickId == this.Brick.Id || joinTable.ChildId == this.Brick.Id)
                        _context.BrickToBrick.Remove(joinTable);
                }


                foreach (var joinTable in _context.BrickCategories)
                {
                    if (joinTable.BrickId == this.Brick.Id)
                        _context.BrickCategories.Remove(joinTable);
                }


                _context.Bricks.Remove(Brick);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
