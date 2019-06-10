using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Worldbuilder.Model;

namespace Worldbuilder.Pages.Bricks
{
    public class CreateModel : PageModel
    {
        private readonly Worldbuilder.Models.WorldbuilderContext _context;

        public CreateModel(Worldbuilder.Models.WorldbuilderContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Brick Brick { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {

            if(await CheckAndSave())
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostImmediateEditAsync()
        {
            if(await CheckAndSave())
            {
                return RedirectToPage("./Edit", new { id = this.Brick.Id });
            }
            else
            {
                return Page();
            }
            
        }


        public async Task<bool> CheckAndSave()
        {
            if(!ModelState.IsValid)
            {
                return false;
            }

            _context.Brick.Add(Brick);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}