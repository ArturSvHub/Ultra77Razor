using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
	public class CreateModel : PageModel
	{
		private readonly MssqlContext _contect;

		public CreateModel(MssqlContext context)
		{
			_contect = context;
		}
		[BindProperty]
		public UpakModelsLibrary.Models.Category Category { get; set; }
		public void OnGet()
		{

		}
		public async Task<IActionResult> OnPost()
		{
			var files = HttpContext.Request.Form.Files;
			Category.Image = await files[0].ImageToImageDataAsync();
			await _contect.Categories.AddAsync(Category);
			await _contect.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
