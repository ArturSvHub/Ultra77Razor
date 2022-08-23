using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UpakModelsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using UpakUtilitiesLibrary;
using Microsoft.AspNetCore.Components;
using UpakDataAccessLibrary.DataContext;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Ultra77Razor.Pages
{
    public partial class NewProduct
    {
        public class CartModel
        {
            public Product? TheProduct { get; set; }
        }

        [Inject]
        public MssqlContext _context { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ProtectedSessionStorage ProtectedSessionStore { get; set; }
        [Parameter]
        public int Id { get; set; }
        
        public bool ExistsInCart { get; set; } = false;
        public List<ProductOption?> Options { get; set; }
        public Dictionary<string, string> SelectedOptions { get; set; }
        public List<ShoppingCart> ShoppingCartsList { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }
        public List<List<SelectListItem>> ListItems { get; set; }
        

        private bool isConnected;
        protected override async Task OnInitializedAsync()
        {
            SelectedOptions = new();
            ShoppingCartsList = new();
            CartModel model = new CartModel();
            TheProduct = await _context.Products!.Include(u => u.Category)
            .FirstOrDefaultAsync(c => c.Id == Id);
            ExistsInCart = false;

            foreach (var item in ShoppingCartsList)
            {
                if (item.ProductId == Id)
                {
                    ExistsInCart = true;
                }
            }
            Keys = new(); Values = new();

            var tempProd = await _context.Products.Include(o => o.ProductOptions).ThenInclude(d => d.OptionDetails).FirstOrDefaultAsync(p => p.Id == Id);
            Options = tempProd.ProductOptions;
            ListItems = new List<List<SelectListItem>>();
            foreach (var item in Options)
            {
                ListItems.Add(new List<SelectListItem>());
            }
            for (int i = 0; i < Options.Count; i++)
            {
                Values.Add("");
                for (int j = 0; j < Options[i].OptionDetails.Count; j++)
                {
                    ListItems[i].Add(new SelectListItem { Value = Options[i].OptionDetails[j].Name, Text = Options[i].OptionDetails[j].Name });
                }
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isConnected = true;
                await LoadStateAsync();
                StateHasChanged();
            }
        }
        private async Task LoadStateAsync()
        {
            var result = await ProtectedSessionStore.GetAsync<List<ShoppingCart>>("cart");
            ShoppingCartsList = result.Success ? result.Value : null;
        }

        private async Task SaveStateAsync()
        {
            await ProtectedSessionStore.SetAsync("cart", ShoppingCartsList);
        }
    }
}
