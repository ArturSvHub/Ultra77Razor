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
		public List<UpakModelsLibrary.Models.Product> products;
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
		}
		public async Task<IActionResult> OnPostAsync()
		{
			var prods = await _context.Products.ToListAsync();
			foreach (var item in Products)
			{
				if(item.IsChecked==true)
				{
					ProductOption.Products.Add(prods.FirstOrDefault(p=>p.Id==item.Id));
				}
			}
			await _context.AddAsync(ProductOption);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
    }
}