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
    public class IndexModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public IndexModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public IList<Brick> Brick { get; set; }
        public IList<BrickCategory> BrickCategories { get; set; }

        public async Task OnGetAsync()
        {
            BrickCategories = await _context.BrickCategories
                .Include(d => d.Category)
                .ToListAsync();

            Brick = await _context.Brick
                .Include(c => c.BrickCategories)
                .Include(p => p.Children)
                .ToListAsync();
        }
    }
}
