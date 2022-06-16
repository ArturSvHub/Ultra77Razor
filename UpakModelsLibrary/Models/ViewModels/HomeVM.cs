using UpakModelsLibrary.Models;

namespace UpakModelsLibrary.Models.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<Product>? Products { get; set; }
		public IEnumerable<Category>? Categories { get; set; }
	}
}
