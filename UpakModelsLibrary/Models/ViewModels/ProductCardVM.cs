using UpakModelsLibrary.Models;

namespace UpakModelsLibrary.Models.ViewModels
{
	public class ProductCardVM
	{
		public ProductCardVM()
		{
			Product = new Product();
		}
		public Product Product{ get; set; }
		public bool ExistsInCart { get; set; } = false;
	}
}
