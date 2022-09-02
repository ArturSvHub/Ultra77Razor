using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.OptionDetails
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
		public async Task<IActionResult> OnPostDelete(int? id)
		{
			if(id is not null)
			{
				var detail = _context.OptionDetails.FirstOrDefault(o => o.Id == id);
				_context.OptionDetails.Remove(detail);
				await _context.SaveChangesAsync();
			}	
			return Redirect("./Options/Index");
		}

	}
}
