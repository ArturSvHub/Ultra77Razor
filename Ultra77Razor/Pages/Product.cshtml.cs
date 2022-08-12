using Blazored.SessionStorage;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Services;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Pages
{
    public class ProductModel : PageModel
    {
		private readonly MssqlContext _context;
		private readonly CartService _cartService;
		private readonly ISessionStorageService _sessionStorage;

		public ProductModel(MssqlContext context,CartService cartService,
			ISessionStorageService sessionStorage)
		{
			_context = context;
			_cartService = cartService;
			_sessionStorage = sessionStorage;
		}

		[BindProperty]
		public Product? Product { get; set; }
        [BindProperty]
        public bool ExistsInCart { get; set; } = false;
		public async Task<IActionResult> OnGetAsync(int id)
        {
			List<ShoppingCart>? shoppingCartsList = new();
			if (HttpContext.Session?.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session?.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}

			Product = await _context.Products!.Include(u => u.Category)
			.FirstOrDefaultAsync(c => c.Id == id);
				ExistsInCart = false;

			foreach (var item in shoppingCartsList)
			{
				if (item.ProductId == id)
				{
					ExistsInCart = true;
				}
			}
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int id)
		{
			List<ShoppingCart> shoppingCartsList = new();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			shoppingCartsList.Add(new ShoppingCart { ProductId = id ,TempCount = Product!.TempCount});
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);

			return RedirectToPage("Index");
		}

	}
}
