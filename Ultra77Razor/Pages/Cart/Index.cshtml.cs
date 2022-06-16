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
		[BindProperty]
		public List<Product>? ProductList { get; set; }
        [BindProperty]
        public ProductUserVM? ProductUserVM { get; set; }
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
			ProductList =await _context.Products.Where(u => prodInCart.Contains(u.Id)).ToListAsync();
			return Page();
		}
        public IActionResult OnPost()
        {
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

	}
}
