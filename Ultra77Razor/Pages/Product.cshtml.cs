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
		[BindProperty]
		public List<ProductOption?> Options { get; set; }
		[BindProperty]
		public Dictionary<string,string> SelectedOptions { get; set; }
		public async Task<IActionResult> OnGetAsync(int id)
        {
			SelectedOptions = new();
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

			var tempProd = await _context.Products.Include(o => o.ProductOptions).ThenInclude(d => d.OptionDetails).FirstOrDefaultAsync(p => p.Id == id);
			Options = tempProd.ProductOptions;
			
			
			
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
			if(Options is not null||Options.Count>0)
			{
				
			}
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);

            return RedirectToPage("Index");
		}

	}
}
