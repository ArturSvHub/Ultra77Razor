using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary.Utility.Extentions;

namespace UpakUtilitiesLibrary.Services
{
	public class CartService
	{
		public int CountOfProducts { get; set; }
		public UltrapackUser AppUser { get; set; } = new();
        public List<ShoppingCart> ShoppingCartList { get; set; }
        public List<Product>? ProductList { get; set; }
		public string ClaimValue { get; private set; }
		private readonly IWebHostEnvironment _environment;
		private readonly IEmailSender _emailSender;
		private readonly MssqlContext _context;
		private readonly NavigationManager _navigationManager;
		private readonly ISessionStorageService _sessionStorage;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CartService(IWebHostEnvironment environment, 
            IEmailSender emailSender, MssqlContext context, NavigationManager navigationManager,
			ISessionStorageService sessionStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _environment = environment;
            _emailSender = emailSender;
            _context = context;
            _navigationManager = navigationManager;
			_sessionStorage = sessionStorage;
			_authenticationStateProvider = authenticationStateProvider;
        }
		public async Task OnInitializedAsync()
        {
			

			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity.IsAuthenticated)
			{
				var claimsIdentity = user.Identity as ClaimsIdentity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				ClaimValue = claim.Value;
				AppUser = await _context.UltrapackUsers.FirstOrDefaultAsync(u => u.Id == ClaimValue);
			}
			else
			{
				_navigationManager.NavigateTo("/Identity/Account/Login");
			}
			
		}
		public async Task OnAfterRenderingAsync()
        {
            ShoppingCartList =await GetShoppingCartList();
			if(ShoppingCartList == null && ShoppingCartList.Count > 0)
            {
				List<int?> prodInCart = ShoppingCartList.Select(i => i.ProductId).ToList();
				List<Product> prodListTemp = await _context.Products!.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

				foreach (var item in ShoppingCartList)
				{
					Product? prodTemp = prodListTemp.FirstOrDefault(u => u.Id == item.ProductId);
					prodTemp.TempCount = item.TempCount.GetValueOrDefault();
					ProductList?.Add(prodTemp);
				}
			}
            
        }
        public async Task OnSubmitAsync()
        {

			foreach (var item in ProductList)
			{
				ShoppingCartList.Add(new ShoppingCart { ProductId = item.Id, TempCount = item.TempCount });
			}
			_sessionStorage.SetToStorage(WebConstants.SessionCart, ShoppingCartList);

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
				UltrapackUserId = ClaimValue,
				FullName = AppUser.FullName,
				Email = AppUser.Email,
				PhoneNumber = AppUser.PhoneNumber,
				OrderDate = DateTime.Now
			};
			await _context.OrderHeaders!.AddAsync(orderHeader);
			await _context.SaveChangesAsync();

			foreach (var prod in ProductList)
			{
				OrderDetails orderDetails = new OrderDetails()
				{
					OrderHeaderId = orderHeader.Id,
					ProductId = prod.Id
				};
				await _context.OrderDetails!.AddAsync(orderDetails);
			}
			await _context.SaveChangesAsync();
			_navigationManager.NavigateTo("/InquiryConfirm");
		}
		public async Task OnPostDeleteAsync(int id)
		{
			if (ShoppingCartList != null && ShoppingCartList.Count > 0)
			{
				ShoppingCartList =await _sessionStorage.GetFromStorage<List<ShoppingCart>>(WebConstants.SessionCart);
			};
			ShoppingCartList?.Remove(ShoppingCartList.FirstOrDefault(u => u.ProductId == id));
			_sessionStorage.ClearAsync();
			_sessionStorage.SetToStorage(WebConstants.SessionCart, ShoppingCartList);
		}
		public void OnPostUpdateAsync()
		{
			foreach (var item in ProductList)
			{
				ShoppingCartList.Add(new ShoppingCart { ProductId = item.Id, TempCount = item.TempCount });
			}
			_sessionStorage.ClearAsync();
			_sessionStorage.SetToStorage(WebConstants.SessionCart, ShoppingCartList);
		}

		//----------------------------------------
		private async Task GetUserClaimValue()
        {
			
		}
		
		private async Task<List<ShoppingCart>> GetShoppingCartList()
        {
			if(ShoppingCartList!=null&&ShoppingCartList.Count>0)
            {
				var varList = await _sessionStorage.GetFromStorage<List<ShoppingCart>>(WebConstants.SessionCart);

				return varList;
            }
            else
            {
				return new List<ShoppingCart>();
			}
            
        }

	}
}
