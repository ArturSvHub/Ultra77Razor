using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.Options
{
    public class CreateModel : PageModel
    {
        private readonly MssqlContext _context;

		public CreateModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public ProductOption ProductOption { get; set; } = new();
		[BindProperty]
		public IEnumerable<SelectListItem>? ProductOptionSelectedList { get; set; }
		[BindProperty]
		public List<OptionDetail> OptionDetails { get; set; }= new();
		public void OnGet()
        {
			ProductOptionSelectedList =_context.Products?.Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}
		public async Task<ActionResult> OnPostAsync()
		{
			await _context.AddAsync(ProductOption);
			await _context.SaveChangesAsync();
			if (OptionDetails.Count > 0)
			{
				foreach(var item in OptionDetails)
				{
					await _context.AddAsync(item);
					await _context.SaveChangesAsync();
				}
			}

			return RedirectToPage("Index");
		}
    }
}
