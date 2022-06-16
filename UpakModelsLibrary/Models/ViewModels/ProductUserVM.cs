using UpakModelsLibrary.Models;

namespace UpakModelsLibrary.Models.ViewModels
{
	public class ProductUserVM
	{
		public ProductUserVM()
		{
			ProductList = new List<Product>();
		}
		public IList<Product>? ProductList { get; set; }
        public UltrapackUser? ApplicationUser { get; set; }
    }
}
