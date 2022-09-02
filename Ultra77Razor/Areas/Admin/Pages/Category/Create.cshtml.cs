using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Category
{
    [Authorize(Roles = WebConstants.AdminRole)]
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
		public async Task<IActionResult> OnPostAsync()
		{
			var files = HttpContext.Request.Form.Files;
			Category.Image = await files[0].ImageToImageDataAsync();
			await _contect.Categories.AddAsync(Category);
			await _contect.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
