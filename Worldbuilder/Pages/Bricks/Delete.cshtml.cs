using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Worldbuilder.Model;
using Worldbuilder.Models;

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

            Brick = await _context.Brick.FirstOrDefaultAsync(m => m.Id == id);

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

            Brick = await _context.Brick.FindAsync(id);

            if (Brick != null)
            {
                _context.Brick.Remove(Brick);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
