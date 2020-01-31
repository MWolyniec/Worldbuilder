using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Helpers;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Bricks
{
    public class EditModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public EditModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;


        }

        [BindProperty]
        public Brick Brick { get; set; }

        [BindProperty]
        public int[] CategorySelect { get; set; }

        [BindProperty]
        public int[] ChildrenSelect { get; set; }

        [BindProperty]
        public int[] ParentsSelect { get; set; }




        private int[] categorySelectionOrig;
        private int[] childrenSelectOrig;
        private int[] parentsSelectOrig;

        public IList<Category> AllCategories { get; set; }

        public IList<Brick> AllBricks { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Children { get; set; }
        public SelectList Parents { get; set; }




        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AllBricks = await _context.Bricks
               .ToListAsync();

            Brick = await _context.Bricks
                .Include(c => c.BrickCategories)
                .Include(p => p.Children)
                .Include(d => d.Parents)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (Brick == null)
            {
                return NotFound();
            }



            AllCategories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();

            Categories = new SelectList(AllCategories, nameof(Category.Id), nameof(Category.Name));
            Children = new SelectList(AllBricks.Where(x => !(x.Id == this.Brick.Id)), nameof(Brick.Id), nameof(Brick.Name));
            Parents = new SelectList(AllBricks.Where(x => !(x.Id == this.Brick.Id)), nameof(Brick.Id), nameof(Brick.Name));




            CategorySelect = Brick.BrickCategories.Select(x => x.CategoryId).ToTwoArrays(out categorySelectionOrig);
            ChildrenSelect = Brick.Children.Select(x => x.ChildId).ToTwoArrays(out childrenSelectOrig);
            ParentsSelect = Brick.Parents.Select(x => x.BrickId).ToTwoArrays(out parentsSelectOrig);

            TempData.Clear();

            TempData.Add(nameof(categorySelectionOrig), categorySelectionOrig);
            TempData.Add(nameof(childrenSelectOrig), childrenSelectOrig);
            TempData.Add(nameof(parentsSelectOrig), parentsSelectOrig);


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
                return RedirectToPage("./Details", new { id = this.Brick.Id });
            else return NotFound();
        }


        public async Task<bool> ChangesCheckedAndSavedSuccessfully()
        {

            ModelChecker.Check(this.ModelState);

            TempData.CorrectEmptyArrays<int>();

            UpdateJoinTableFromSelectList.Update(_context.BrickCategories, Brick, TempData[nameof(categorySelectionOrig)] as int[], CategorySelect);

            UpdateJoinTableFromSelectList.Update(_context.BrickToBrick, Brick, TempData[nameof(childrenSelectOrig)] as int[], ChildrenSelect);

            UpdateJoinTableFromSelectList.Update(_context.BrickToBrick, Brick, TempData[nameof(parentsSelectOrig)] as int[], ParentsSelect, Relationship.Reversed);

            TempData.Clear();

            return await SaveChanges.SavedSuccessfully(this._context, this._context.Bricks, this.Brick);

            /*
            _context.Attach(Brick).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bricks.Any(e => e.Id == Brick.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }

            }
            return true;*/
        }

    }
}
