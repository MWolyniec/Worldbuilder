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
    public class DetailsModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public DetailsModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public Brick Brick { get; set; }
        public IList<BrickCategory> BrickCategories { get; set; }
        public IList<BrickToBrick> ChildrenJoinTable { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChildrenJoinTable = await _context.BrickToBrick
                .Include(z => z.Child)
                .ToListAsync();

            BrickCategories = await _context.BrickCategories
                .Include(d => d.Category)
                .ToListAsync();

            Brick = await _context.Brick
                .Include(c => c.BrickCategories)
                .Include(p => p.Children).
                FirstOrDefaultAsync(m => m.Id == id);

            if (Brick == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
