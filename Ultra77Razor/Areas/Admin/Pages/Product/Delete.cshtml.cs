using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.Product
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class DeleteModel : PageModel
    {
        private readonly MssqlContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(MssqlContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
	}
		[BindProperty]
        public UpakModelsLibrary.Models.Product? Product { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var obj = await _context.Products.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
