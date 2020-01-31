using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Bricks
{
    public class IndexModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public IndexModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public PaginatedList<Brick> Brick { get; set; }
        public IList<BrickCategory> BrickCategories { get; set; }


        #region Sort

        public string NameSort { get; set; }
        public string CategorySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        #endregion

        #region Search
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BrickCategory { get; set; }
        #endregion

        public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CategorySort = sortOrder == "Category" ? "category_desc" : "Category";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var brickCategories = from d in _context.BrickCategories
                                  select d;



            var catTypesGroups = await _context.CategoryTypes.Select(x => new SelectListGroup { Name = x.Name }).ToListAsync();

            var orderedCategories = await _context.Categories
                .Include(x => x.CategoryType)
                .OrderBy(o => o.CategoryType.Name)
                .ThenBy(t => t.Name)
                .ToListAsync();
            /*
                .Select
                (x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Group = catTypesGroups.First(d => d.Name.Equals(x.CategoryType.Name))
                }
                ).ToListAsync();*/

            Categories = new List<SelectListItem>();
            foreach (var category in orderedCategories)
            {
                var newSLItem = (new SelectListItem()
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
                if (category.CategoryType != null)
                    if (category.CategoryType.Name != null)
                        newSLItem.Group = catTypesGroups.FirstOrDefault(d => d.Name.Equals(category.CategoryType.Name));

                Categories.Add(newSLItem);
            }
            BrickCategories = await brickCategories.Include(d => d.Brick).Include(d => d.Category).ToListAsync();


            var bricks = _context.Bricks
                .Include(d => d.BrickCategories)
                .ThenInclude(b => b.Category)
                .Select(m => m);

            if (!string.IsNullOrEmpty(SearchString))
            {
                bricks = bricks.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(BrickCategory))
            {
                bricks = bricks
                    .Where
                    (
                    x => x.BrickCategories.Any(b => b.CategoryId == Convert.ToInt32(BrickCategory))
                    );
            }


            switch (sortOrder)
            {
                case "name_desc":
                    bricks = bricks.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    bricks = bricks.OrderBy(s => s.BrickCategories.OrderBy(bc => bc.Category.Name).FirstOrDefault().Category.Name);
                    brickCategories = brickCategories.OrderBy(x => x.Category.Name);
                    break;
                case "category_desc":
                    bricks = bricks.OrderByDescending(s => s.BrickCategories.OrderBy(bc => bc.Category.Name).LastOrDefault().Category.Name);
                    brickCategories = brickCategories.OrderByDescending(x => x.Category.Name);
                    break;
                default:
                    bricks = bricks.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 10;
            Brick = await PaginatedList<Brick>.CreateAsync(
                bricks.Include(c => c.Children).ThenInclude(ch => ch.Child).Include(p => p.Parents).ThenInclude(pa => pa.Brick).AsNoTracking(),
                pageIndex ?? 1, pageSize);




        }
    }
}
