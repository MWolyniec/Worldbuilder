using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        

        #region Search
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BrickCategory { get; set; }
        #endregion

        public async Task OnGetAsync()
        {
            BrickCategories = await _context.BrickCategories
                .Include(d => d.Category)
                .ToListAsync();

            
            var catTypesGroups = await _context.CategoryTypes.Select(x => new SelectListGroup { Name = x.Name }).ToListAsync();

            Categories = await _context.Categories
                .OrderBy(o => o.CategoryType.Name)
                .ThenBy(t => t.Name)
                .Select
                (x => 
                new SelectListItem {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Group = catTypesGroups.First(d => d.Name.Equals(x.CategoryType.Name))
                    }
                )
                .ToListAsync();    
            
            var bricks = from m in _context.Brick
                         select m;
            if(!string.IsNullOrEmpty(SearchString))
            {
                bricks = bricks.Where(s => s.Name.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(BrickCategory))
            {
                bricks = bricks
                    .Where
                    (
                    x => x.BrickCategories.Any(b => b.CategoryId == Convert.ToInt32(BrickCategory))
                    
                    );
            }

            Brick = await bricks
                .ToListAsync();


        }
    }
}
