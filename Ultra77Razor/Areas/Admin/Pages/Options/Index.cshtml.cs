using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.Options
{
    public class IndexModel : PageModel
    {
        private readonly MssqlContext _context;

		public IndexModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public List<ProductOption> ProductOptions { get; set; } = new();
		[BindProperty]
		public List<OptionDetail> OptionDetails { get; set; } = new();
		public async Task<ActionResult> OnGetAsync()
        {
			ProductOptions =await _context.ProductOptions.ToListAsync();
			OptionDetails = await _context.OptionDetails.ToListAsync();
			return Page();
        }
    }
}
