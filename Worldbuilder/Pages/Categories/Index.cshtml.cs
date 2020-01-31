using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public IndexModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public PaginatedList<Category> Categories { get; set; }

        #region Sort

        public string NameSort { get; set; }
        public string TypeSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        #endregion

        #region Search
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<SelectListItem> CategoryTypesAsSelectList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CategoryTypeAsString { get; set; }
        #endregion

        public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TypeSort = sortOrder == "Type" ? "type_desc" : "Type";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;


            CategoryTypesAsSelectList = await _context.CategoryTypes
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


            var categories = _context.Categories.Include(d => d.CategoryType).Select(x => x);

            if (!string.IsNullOrEmpty(SearchString))
            {
                categories = categories.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(CategoryTypeAsString))
            {
                categories = categories
                    .Where
                    (
                    x => x.CategoryType.Id == Convert.ToInt32(CategoryTypeAsString)
                    );
            }



            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(s => s.Name);
                    break;
                case "Type":
                    categories = categories.OrderBy(s => s.CategoryType.Name);
                    break;
                case "type_desc":
                    categories = categories.OrderByDescending(s => s.CategoryType.Name);
                    break;
                default:
                    categories = categories.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            Categories = await PaginatedList<Category>.CreateAsync(
                categories,
                pageIndex ?? 1, pageSize);


        }
    }
}
