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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.Brick.Add(Brick);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
        /*
        public async Task<IActionResult> OnPostImmediateEditAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _context.Brick.Add(Brick);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Edit", new {id = this.Brick.Id});
        }*/

    }
}