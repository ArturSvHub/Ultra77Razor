using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.Text;

using UpakDataAccessLibrary.DataContext;
using UpakModelsLibrary.Models;
using UpakModelsLibrary.Models.ViewModels;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Services;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Pages.Cart
{
	[ValidateAntiForgeryToken]
	public class IndexModel : PageModel
	{
		[BindProperty]
		public UltrapackUser AppUser { get; set; } = new();
		[BindProperty]
		public List<Product>? ProductList { get; set; } = new();
		[BindProperty]
		public CartService CartService { get; set; } = new();
		private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;
		private readonly MssqlContext _context;
		private readonly CartService _cartService;

		public IndexModel(IWebHostEnvironment environment, 
							IEmailSender emailSender, 
							MssqlContext context, 
							CartService cartService)
		{
			_environment = environment;
			_emailSender = emailSender;
			_context = context;
			_cartService = cartService;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


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

			AppUser =_context.UltrapackUsers?.FirstOrDefault(u => u.Id == claim.Value);

			return Page();
		}



        public async Task<IActionResult> OnPostAsync()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			AppUser = _context.UltrapackUsers?.FirstOrDefault(u => u.Id == claim.Value);
			List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
			
			foreach (var item in ProductList)
	        {
		        shoppingCartList.Add(new ShoppingCart { ProductId = item.Id, TempCount = item.TempCount });
				
			}
	        HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);

			var PathToTemplate = _environment.WebRootPath + Path.DirectorySeparatorChar.ToString() +
				"templates" + Path.DirectorySeparatorChar.ToString() +
				"inquiry.html";
			var subject = "Новый заказ";
			string HtmlBody = "";
			using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
			{
				HtmlBody = sr.ReadToEnd();
			}
			StringBuilder productListSB = new StringBuilder();
			foreach (var item in ProductList)
			{
				productListSB.Append($" - {item.Name} <span style='font-size:14px;' (ID: {item.Id})</span><span style='font-size:14px;'>{item.TempCount}</span></br>");
			}
			string messageBody = string.Format(HtmlBody,
				
				AppUser.FullName,
				AppUser.Email,
				AppUser.PhoneNumber,
				productListSB.ToString()
				);

			await _emailSender.SendEmailAsync(WebConstants.EmailForEnquires, subject, messageBody);

			OrderHeader orderHeader = new OrderHeader()
			{
				UltrapackUserId = claim.Value,
				FullName = AppUser.FullName,
				Email = AppUser.Email,
				PhoneNumber = AppUser.PhoneNumber,
				OrderDate = DateTime.Now
			};
			await _context.OrderHeaders.AddAsync(orderHeader);
			await _context.SaveChangesAsync();

			foreach (var prod in ProductList)
			{
				OrderDetails orderDetails = new OrderDetails()
				{
					OrderHeaderId = orderHeader.Id,
					ProductId = prod.Id
				};
				await _context.OrderDetails.AddAsync(orderDetails);
			}
			await _context.SaveChangesAsync();

			return RedirectToPage("InquiryConfirm");
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
				shoppingCartList.Add(new ShoppingCart { ProductId = item.Id, TempCount = item.TempCount });
			}
			HttpContext.Session.Set(WebConstants.SessionCart,shoppingCartList);
			return RedirectToPage("Index");
		}
		private void stepUp()
		{

		}
		private void stepDown()
		{

		}

	}
}
