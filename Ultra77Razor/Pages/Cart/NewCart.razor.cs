using Microsoft.AspNetCore.Components;

using UpakUtilitiesLibrary.Services;

namespace Ultra77Razor.Pages.Cart
{
	public partial class NewCart
	{
        [Inject]
        public CartService CartServiceItem { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CartServiceItem.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CartServiceItem.OnAfterRenderingAsync();
        }
    }
}
