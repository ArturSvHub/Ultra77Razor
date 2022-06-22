using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UpakDataAccessLibrary.DataContext;
using UpakModelsLibrary.Models;
using UpakModelsLibrary.Models.ViewModels;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Pages.Cart
{
	public class IndexModel : PageModel
	{
		[BindProperty] public List<Product>? ProductList { get; set; } = new();
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;
		private readonly MssqlContext _context;

		public IndexModel(IWebHostEnvironment environment, IEmailSender emailSender, MssqlContext context)
		{
			_environment = environment;
			_emailSender = emailSender;
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			List<ShoppingCart>? shoppingCartList = new List<ShoppingCart>();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			List<int?> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
			List<Product> prodListTemp = await _context.Products.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

			foreach (var item in shoppingCartList)
			{
				Product prodTemp = prodListTemp.FirstOrDefault(u => u.Id == item.ProductId);
				prodTemp.TempCount = item.TempCount;
				ProductList.Add(prodTemp);
			}

			return Page();
		}
        public IActionResult OnPost()
        {
	        List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
	        foreach (var item in ProductList)
	        {
		        shoppingCartList.Add(new ShoppingCart { ProductId = item.Id, TempCount = item.TempCount });
	        }
	        HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
			return RedirectToPage("Summary");
        }
		public IActionResult OnPostDeleteAsync(int id)
		{
			List<ShoppingCart>? shoppingCartList = new();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			shoppingCartList?.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
			return Page();
		}
		public IActionResult OnPostUpdateAsync()
		{
			List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
			foreach (var item in ProductList)
			{
				shoppingCartList.Add(new ShoppingCart{ProductId = item.Id,TempCount = item.TempCount});
			}
			HttpContext.Session.Set(WebConstants.SessionCart,shoppingCartList);
			return RedirectToPage("Index");
		}

	}
}
