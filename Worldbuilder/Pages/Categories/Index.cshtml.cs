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

namespace Worldbuilder.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public IndexModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }
        
        #region Search
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<SelectListItem> CategoryTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CategoryType { get; set; }
        #endregion

        public async Task OnGetAsync()
        {
            CategoryTypes = await _context.CategoryTypes
                .OrderBy(o => o.Name)
                .Select
                (x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }
                )
                .ToListAsync();


            var categories = from m in _context.Categories
                         select m;

            if(!string.IsNullOrEmpty(SearchString))
            {
                categories = categories.Where(s => s.Name.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(CategoryType))
            {
                categories = categories
                    .Where
                    (
                    x => x.CategoryType.Id == Convert.ToInt32(CategoryType)
                    );
            }

            Category = await categories.ToListAsync();
        }
    }
}
