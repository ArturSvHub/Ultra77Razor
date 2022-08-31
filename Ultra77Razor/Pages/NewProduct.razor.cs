using UpakModelsLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using UpakDataAccessLibrary.Repository.Interfases;

namespace Ultra77Razor.Pages
{
    public partial class NewProduct
    {

        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        public ProtectedSessionStorage? ProtectedSessionStore { get; set; }
        [Inject]
        public IProductRepository? ProductRepository { get; set; }
        [Parameter]
        public List<ShoppingCart>? ShoppingCartsList { get; set; } = new();
        [Parameter]
        public int Id { get; set; }
        public Product? Product { get; set; } = new();
        public Product? ProductWithOptions { get; set; }

        private ShoppingCart? thisShopProduct = new() { TempCount=1};
        private decimal retailprice;
        private bool isConnected;
        private int? countThisProducts;

        private async Task LoadStateAsync()
        {
            var result = await ProtectedSessionStore!.GetAsync<List<ShoppingCart>>("cart");
            ShoppingCartsList = result.Success ? result.Value : null;
        }

        private async Task SaveStateAsync()
        {
            await ProtectedSessionStore!.SetAsync("cart", ShoppingCartsList!);
        }

        protected override async Task OnInitializedAsync()
        {
            Product = await ProductRepository!.GetProductWithDetailsByIdAsync(Id);
            retailprice = ((decimal?)Product.RetailPrice) ?? 0;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isConnected = true;
                await LoadStateAsync();

                //if(ShoppingCartsList is not null && ShoppingCartsList.Count>0)
                //{
                //    thisShopProduct = ShoppingCartsList.FirstOrDefault(s=>s.ProductId==Id);
                //    countThisProducts = thisShopProduct?.TempCount;
                //}
                //else
                //{
                //    countThisProducts = 1;
                //}
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
