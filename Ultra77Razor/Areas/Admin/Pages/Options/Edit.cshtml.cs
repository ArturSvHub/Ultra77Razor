using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.Options
{
    public class EditModel : PageModel
    {
        private readonly MssqlContext _context;

        public EditModel(MssqlContext context)
        {
            _context = context;
        }

		[BindProperty]
		public ProductOption Option { get; set; } = new();
		[BindProperty]
		public IEnumerable<SelectListItem>? ProductOptionSelectedList { get; set; }
		[BindProperty]
		public List<OptionDetail> OptionDetails { get; set; } = new();
		public async Task<ActionResult> OnGet(int? id)
		{
			Option =await _context.ProductOptions.FindAsync(id);
			if (Option != null)
			{
				; ProductOptionSelectedList = _context.Products?.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				});
				OptionDetails = await _context.OptionDetails.Include(o => o.ProductOption).Where(o => o.ProductOptionId == Option.Id).ToListAsync();
			}
				return Page();
		}
	}
}
