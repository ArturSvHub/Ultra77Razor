using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Text;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary;
using UpakUtilitiesLibrary.Services;
using UpakUtilitiesLibrary.Utility.Extentions;

namespace Ultra77Razor.Pages.Cart
{
	[Authorize]
	[ValidateAntiForgeryToken]
	public class IndexModel : PageModel
	{
		[BindProperty]
		public UltrapackUser AppUser { get; set; }
		[BindProperty]
		public List<UpakModelsLibrary.Models.Product>? ProductList { get; set; }
		[BindProperty]
		public List<Dictionary<string, string>> ProductOptions { get; set; } = new();

		private readonly IWebHostEnvironment _environment;
		private readonly IEmailSender _emailSender;
		private readonly MssqlContext _context;

		public IndexModel(IWebHostEnvironment environment,
							IEmailSender emailSender,
							MssqlContext context,
							CartService cartService)
		{
			_environment = environment;
			_emailSender = emailSender;
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			ProductList = new();
			AppUser = await _context.UltrapackUsers.SingleAsync(u => u.UserName == User.Identity.Name);
			List<ShoppingCart>? shoppingCartList = new List<ShoppingCart>();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}

            List<int?> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            List<Product> prodListTemp = await _context.Products!.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

			foreach (var item in shoppingCartList)
			{
				ProductOptions.Add(item.ProductOptions);
				Product prodTemp = prodListTemp.FirstOrDefault(u => u.Id == item.ProductId);
				prodTemp.TempCount = item.TempCount.GetValueOrDefault();
				ProductList.Add(prodTemp);
			}

			return Page();
		}


		
		public async Task<IActionResult> OnPostAsync()
		{
			AppUser = await _context.UltrapackUsers!.SingleAsync(u => u.UserName == User.Identity!.Name);
			var userId = AppUser.Id;
			List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            List<int?> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            List<Product> prodListTemp = await _context.Products!.Where(u => prodInCart.Contains(u.Id)).ToListAsync();
			ProductList.Clear();
            foreach (var item in shoppingCartList)
            {
                ProductOptions.Add(item.ProductOptions);
                Product prodTemp = prodListTemp.FirstOrDefault(u => u.Id == item.ProductId);
                prodTemp.TempCount = item.TempCount.GetValueOrDefault();
                ProductList.Add(prodTemp);
            }
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
			StringBuilder optionsSB = new StringBuilder();
			for (int i = 0; i < ProductList.Count; i++)
			{
				if(shoppingCartList[i] is not null && shoppingCartList[i].ProductOptions is not null)
				{
                    foreach (var item in shoppingCartList[i].ProductOptions)
                    {
                        optionsSB.Append($"<p>{item.Key} - {item.Value}</p>");
                    }
                }
				
				productListSB.Append($"<span style='font-size:14px;'>Id - {ProductList[i].Id}; Название - {ProductList[i].Name}; к-во - {ProductList[i].TempCount} шт; </span>" +
					$"</br><span style='font-size:14px;'>Характеристики - {optionsSB}</span></br></br>");
				optionsSB.Clear();
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
				UltrapackUserId = userId,
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


		public ActionResult OnPostDelete(int id)
		{
			List<ShoppingCart>? shoppingCartList = new();
			if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
				HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
			{
				shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
			}
			shoppingCartList?.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
			return RedirectToPage("Index");
		}
		public IActionResult OnPostUpdateAsync()
		{
			List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
			foreach (var item in shoppingCartList)
			{
                ProductOptions.Add(item.ProductOptions);
            }
			shoppingCartList.Clear();
            for (int i=0;i<ProductList.Count;i++)
			{
				shoppingCartList.Add(new ShoppingCart { ProductId = ProductList[i].Id, TempCount = ProductList[i].TempCount, ProductOptions = ProductOptions[i] });
			}
			HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
			return RedirectToPage("Index");
		}
	}
}
