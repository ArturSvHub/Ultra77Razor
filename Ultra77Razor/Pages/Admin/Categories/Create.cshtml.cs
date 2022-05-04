#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ultra77Razor.DataContext;
using Ultra77Razor.Models;

namespace Ultra77Razor.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly Ultra77Razor.DataContext.MssqlContext _context;

        public CreateModel(Ultra77Razor.DataContext.MssqlContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            if (!ModelState.IsValid||image==null||image.Length==0)
            {
                return Page();
            }
            using(var targetms = new MemoryStream())
			{
                image.CopyTo(targetms);
                Category.Image = targetms.ToArray();
			}

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
