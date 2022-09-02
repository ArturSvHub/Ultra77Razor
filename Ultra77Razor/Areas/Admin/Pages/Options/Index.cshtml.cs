using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.Options
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
        private readonly MssqlContext _context;

		public IndexModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public List<ProductOption> ProductOptions { get; set; }
		[BindProperty]
		public List<OptionDetail> OptionDetails { get; set; }
		public async Task<ActionResult> OnGetAsync()
        {
			ProductOptions =await _context.ProductOptions.Include(p=>p.Products).ToListAsync();
			OptionDetails = await _context.OptionDetails.Include(p => p.ProductOption).ToListAsync();
			return Page();
        }
		public async Task<ActionResult> OnPostDeleteAsync(int id)
		{
			var option =await _context.ProductOptions.FindAsync(id); 
			_context.Remove(option);
			await _context.SaveChangesAsync();
			return RedirectToPage("./Index");
		}
	}
}
