using Blazored.SessionStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		public Product? TheProduct { get; set; }
		[BindProperty]
		public bool ExistsInCart { get; set; } = false;
		[BindProperty]
		public List<ProductOption?> Options { get; set; }
		[BindProperty]
		public Dictionary<string,string> SelectedOptions { get; set; }
		[BindProperty]
		public List<string> Keys { get; set; }
		[BindProperty]
		public List<string> Values { get; set; }
		[BindProperty]
		public List<List<SelectListItem>> ListItems { get; set; }
		public async Task<IActionResult> OnGetAsync(int id)
		{
			SelectedOptions = new();
			List<ShoppingCart>? shoppingCartsList = new();
			if (HttpContext.Session?.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session?.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			TheProduct = await _context.Products!.Include(u => u.Category)
			.FirstOrDefaultAsync(c => c.Id == id);
				ExistsInCart = false;

			foreach (var item in shoppingCartsList)
			{
				if (item.ProductId == id)
				{
					ExistsInCart = true;
				}
			}

			Keys = new();Values = new();

			var tempProd = await _context.Products.Include(o => o.ProductOptions).ThenInclude(d => d.OptionDetails).FirstOrDefaultAsync(p => p.Id == id);
			Options = tempProd.ProductOptions;
			ListItems = new List<List<SelectListItem>>();
			foreach (var item in Options)
			{
				ListItems.Add(new List<SelectListItem>());
			}
				for (int i=0;i<Options.Count;i++)
			{
				Values.Add("");
				for(int j = 0; j < Options[i].OptionDetails.Count;j++)
				{
					ListItems[i].Add(new SelectListItem { Value = Options[i].OptionDetails[j].Name, Text = Options[i].OptionDetails[j].Name });
				}
			}

			

			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int id)
		{
			var tempProd = await _context.Products.Include(o => o.ProductOptions).ThenInclude(d => d.OptionDetails).FirstOrDefaultAsync(p => p.Id == id);
			SelectedOptions.Clear();
			for (int i=0;i<tempProd.ProductOptions.Count;i++)
			{
				Keys.Add(tempProd.ProductOptions[i].Name);
				SelectedOptions.Add(Keys[i], Values[i]);
			}
			List<ShoppingCart> shoppingCartsList = new();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			shoppingCartsList.Add(new ShoppingCart { ProductId = id ,TempCount = TheProduct!.TempCount,ProductOptions=SelectedOptions});
			
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);

			return RedirectToPage("Index");
		}

	}
}
