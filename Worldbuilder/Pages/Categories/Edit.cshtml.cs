using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Helpers;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public EditModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public int? CategoryTypeSelection { get; set; }


        private int? categoryTypeSelectionOrig;

        public IList<CategoryType> AllCategoryTypes { get; set; }


        public SelectList CategoryTypesSelectList { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);


            if (Category == null)
            {
                return NotFound();
            }


            AllCategoryTypes = await _context.CategoryTypes.OrderBy(x => x.Name).ToListAsync();

            CategoryTypesSelectList = new SelectList(AllCategoryTypes, nameof(CategoryType.Id), nameof(CategoryType.Name));

            if (Category.CategoryType != null)
            {
                CategoryTypeSelection = Category.CategoryType.Id;
                categoryTypeSelectionOrig = Category.CategoryType.Id;
            }



            TempData.Clear();
            TempData.Add(nameof(categoryTypeSelectionOrig), categoryTypeSelectionOrig);

            return Page();

        }

        public async Task<IActionResult> OnPostToIndexAsync()
        {
            if (await ChangesCheckedAndSavedSuccessfully())

                return RedirectToPage("./Index");
            else return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (await ChangesCheckedAndSavedSuccessfully())
                return RedirectToPage("./Details", new { id = this.Category.Id });
            else return NotFound();
        }



        public async Task<bool> ChangesCheckedAndSavedSuccessfully()
        {

            ModelChecker.Check(this.ModelState);

            TempData.CorrectEmptyArrays<int>();

            if (CategoryTypeSelection != null)
                Category.CategoryType = _context.CategoryTypes.FirstOrDefault(x => x.Id == CategoryTypeSelection);
            else
                Category.CategoryType = null;


            TempData.Clear();

            return await SaveChanges.SavedSuccessfully(this._context, this._context.Categories, this.Category);
        }

    }
}
