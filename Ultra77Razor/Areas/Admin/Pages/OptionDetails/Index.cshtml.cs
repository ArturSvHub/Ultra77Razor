using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.OptionDetails
{
    public class IndexModel : PageModel
    {
        private readonly MssqlContext _context;

		public IndexModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public ProductOption Option { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id)
        {
			if(id!=null)
			{
				Option = await _context.ProductOptions.Include(o => o.OptionDetails).FirstOrDefaultAsync(d => d.Id == id);
				return Page();
			}
			else
			{
				return NotFound();
			}
        }
    }
}
