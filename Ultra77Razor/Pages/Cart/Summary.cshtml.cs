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
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Pages.Cart
{
	public class SummaryModel : PageModel
	{
		private readonly MssqlContext _context;
		private readonly IWebHostEnvironment _environment;
		private readonly IEmailSender _emailSender;

		public SummaryModel(MssqlContext context, IWebHostEnvironment environment, IEmailSender emailSender)
		{
			_context = context;
			_environment = environment;
			_emailSender = emailSender;
		}
		public ProductUserVM? ProductUserVM { get; set; }
		[BindProperty]
		public List<Product> ProductList { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			List<int?> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
			ProductList =_context.Products!.Where(u => prodInCart.Contains(u.Id)).ToList();

			ProductUserVM = new ProductUserVM()
			{
				ApplicationUser = _context.UltrapackUsers.FirstOrDefault(u => u.Id == claim.Value),
				ProductList = ProductList
			};
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(ProductUserVM productUserVM)
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);


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
			foreach (var item in productUserVM.ProductList)
			{
				productListSB.Append($" - {item.Name} <span style='font-size:14px;' (ID: {item.Id})</span></br>");
			}
			string messageBody = string.Format(HtmlBody,
				ProductUserVM.ApplicationUser?.FullName,
				ProductUserVM.ApplicationUser?.Email,
				ProductUserVM.ApplicationUser?.PhoneNumber,
				productListSB.ToString()
				);

			await _emailSender.SendEmailAsync(WebConstants.EmailForEnquires, subject, messageBody);
			
			OrderHeader orderHeader = new OrderHeader()
			{
				UltrapackUserId = claim.Value,
				FullName = ProductUserVM.ApplicationUser?.FullName,
				Email = ProductUserVM.ApplicationUser?.Email,
				PhoneNumber = ProductUserVM.ApplicationUser?.PhoneNumber,
				OrderDate = DateTime.Now
			};
			await _context.OrderHeaders.AddAsync(orderHeader);
			_context.SaveChanges();

			foreach (var prod in ProductUserVM.ProductList)
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
	}
}
