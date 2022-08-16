using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

namespace Ultra77Razor.Areas.Admin.Pages.Options
{
    public class CreateModel : PageModel
    {
        private readonly MssqlContext _context;
		List<UpakModelsLibrary.Models.Product> products;
		public CreateModel(MssqlContext context)
		{
			_context = context;
		}
		[BindProperty]
		public ProductOption ProductOption { get; set; }
		[BindProperty]
		public List<UpakModelsLibrary.Models.Product> Products { get; set; }

		public void OnGet()
        {
			ProductOption = new ProductOption();
			Products = _context.Products.ToList();
			products = Products;
		}
		public async Task<IActionResult> OnPostAsync()
		{
			foreach (var item in Products)
			{
				if(item.IsChecked==true)
				{
					ProductOption.Products.Add(item);
				}
			}
			await _context.AddAsync(ProductOption);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
    }
}