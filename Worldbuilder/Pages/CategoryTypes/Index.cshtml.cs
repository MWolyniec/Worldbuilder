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

        #region Sort

        public string NameSort { get; set; }
        public string CategorySort { get; set; }
        #endregion


        public IList<CategoryType> CategoryType { get;set; }
        public IList<Category> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var catTypes = from m in _context.CategoryTypes
                           select m;
            var categories = from m in _context.Categories
                             select m;


            if(!string.IsNullOrEmpty(SearchString))
            {
                catTypes = catTypes.Where(s => s.Name.Contains(SearchString));
            }

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CategorySort = sortOrder == "Category" ? "category_desc" : "Category";


            switch(sortOrder)
            {
                case "name_desc":
                    catTypes = catTypes.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    catTypes = catTypes.OrderBy(s => s.Categories.OrderBy(bc => bc.Name).FirstOrDefault().Name);
                    categories = categories.OrderBy(x => x.Name);
                    break;
                case "category_desc":
                    catTypes = catTypes.OrderByDescending(s => s.Categories.OrderBy(bc => bc.Name).LastOrDefault().Name);
                    categories = categories.OrderByDescending(x => x.Name);
                    break;
                default:
                    catTypes = catTypes.OrderBy(s => s.Name);
                    break;
            }

            CategoryType = await catTypes.Include(x => x.Categories).ToListAsync();
            Categories = await categories.ToListAsync();
        }
    }
}
