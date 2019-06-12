using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Worldbuilder.Model;
using Worldbuilder.Models;

namespace Worldbuilder.Pages.CategoryTypes
{
    public class IndexModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public IndexModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public IList<CategoryType> CategoryType { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var catTypes = from m in _context.CategoryTypes
                           select m;

            if(!string.IsNullOrEmpty(SearchString))
            {
                catTypes = catTypes.Where(s => s.Name.Contains(SearchString));
            }
            
            CategoryType = await catTypes.Include(x => x.Categories).ToListAsync();
        }
    }
}
