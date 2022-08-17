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
		public ProductOption Option { get; set; }
		[BindProperty]
		public List<UpakModelsLibrary.Models.Product> Products { get; set; }
		public ActionResult OnGet(int id)
		{
			var Options = _context.ProductOptions.Include(p => p.Products).ToList();
			Option = Options.FirstOrDefault(o => o.Id == id);
			Products = _context.Products.AsNoTracking().ToList();
			foreach (var opt in Option.Products!)
			{
				foreach (var product in Products)
				{
					if (product.Id == opt.Id)
					{
						product.IsChecked = true;
					}
				}
			}


			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int id)
		{
			
			var prods = await _context.Products.Include(o=>o.ProductOptions).AsNoTracking().ToListAsync();
			var Options = _context.ProductOptions.Include(p => p.Products).AsNoTracking().ToList();
			Option = Options.FirstOrDefault(o => o.Id == id);

			for (int i = 0; i < Products.Count; i++)
			{
				if (Products[i].IsChecked == true & !Option.Products.Exists(c => c.Id == Products[i].Id))
				{
					Option.Products.Add(prods.FirstOrDefault(c => c.Id == Products[i].Id));
				}
				if(Products[i].IsChecked == false & Option.Products.Exists(c => c.Id == prods[i].Id))
				{
					Option.Products.Remove(Option.Products.FirstOrDefault(c => c.Id == prods[i].Id));
				}
			}



			_context.ProductOptions.Update(Option);
			await _context.SaveChangesAsync();

			return RedirectToPage("Index");
		}
	}
}
