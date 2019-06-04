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
    public class EditModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public EditModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
            /*  _categorySelectionOrig = new int[1] { 0 };
              _childrenSelectOrig = new int[1] { 0 };
              _parentsSelectOrig = new int[1] { 0 };*/

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
            if(id == null)
            {
                return NotFound();
            }
            AllBricks = await _context.Brick
               .ToListAsync();

            Brick = await _context.Brick
                .Include(c => c.BrickCategories)
                .Include(p => p.Children)
                .Include(d => d.Parents)
                .FirstOrDefaultAsync(m => m.Id == id);


            if(Brick == null)
            {
                return NotFound();
            }



            AllCategories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();

            Categories = new SelectList(AllCategories, nameof(Category.Id), nameof(Category.Name));
            Children = new SelectList(AllBricks, nameof(Brick.Id), nameof(Brick.Name));
            Parents = new SelectList(AllBricks, nameof(Brick.Id), nameof(Brick.Name));

            CategorySelect = Brick.BrickCategories.Select(x => x.CategoryId).ToTwoArrays(out categorySelectionOrig);
            ChildrenSelect = Brick.Children.Select(x => x.ChildId).ToTwoArrays(out childrenSelectOrig);
            ParentsSelect = Brick.Parents.Select(x => x.BrickId).ToTwoArrays(out parentsSelectOrig);

            TempData.Clear();

            TempData.Add(nameof(categorySelectionOrig), categorySelectionOrig);
            TempData.Add(nameof(childrenSelectOrig), childrenSelectOrig);
            TempData.Add(nameof(parentsSelectOrig), parentsSelectOrig);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                var err = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return Page();
            }



            TempData.CorrectEmptyArrays<int>();

            await _context.BrickCategories.UpdateFromSelectList
                (Brick, TempData[nameof(categorySelectionOrig)] as int[], CategorySelect);

            await _context.BrickToBrick.UpdateFromSelectList
                (Brick, TempData[nameof(childrenSelectOrig)] as int[], ChildrenSelect);

            await _context.BrickToBrick.UpdateFromSelectList
                (Brick, TempData[nameof(parentsSelectOrig)] as int[], ParentsSelect, Relationship.Reversed);

            TempData.Clear();

            _context.Attach(Brick).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!BrickExists(Brick.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BrickExists(int id)
        {
            return _context.Brick.Any(e => e.Id == id);
        }
    }
}
