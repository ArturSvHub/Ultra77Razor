using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Areas.Admin.Pages.Product
{
	[Authorize(Roles = WebConstants.AdminRole)]
	public class UpsertModel : PageModel
	{
		private readonly MssqlContext _context;
		private readonly IWebHostEnvironment _environment;

		public UpsertModel(MssqlContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}
		[BindProperty]
		public UpakModelsLibrary.Models.Product? Product { get; set; }
		[BindProperty]
		public IEnumerable<SelectListItem>? CategorySelectedList { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id,UpakModelsLibrary.Models.Product? product=null)
		{
			Product = new UpakModelsLibrary.Models.Product();
			CategorySelectedList = _context.Categories?.Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			if (id == null)
			{
				return Page();
			}
			else
			{
				Product =await _context.Products.FindAsync(id);
				if (Product ==null)
				{
					return NotFound();
				}
				product = Product;
				return Page();
			}

		}
		public async Task<IActionResult> OnPostAsync(UpakModelsLibrary.Models.Product? product)
		{
			var files = HttpContext.Request.Form.Files;

			if (product.Id == 0)
			{
				product.Image = await files[0].ImageToImageDataAsync();

				await _context.AddAsync(product);
			}
			else
			{
				if (files.Count!=0)
				{
					product.Image = await files[0].ImageToImageDataAsync();
				}
				else
				{
					var objFromDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
					product.Image = objFromDb?.Image;
				}
				_context.Update(product);
			}
			await _context.SaveChangesAsync();

			return RedirectToPage("Index");
		}
	}
}
