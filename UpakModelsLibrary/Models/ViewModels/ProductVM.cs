using Microsoft.AspNetCore.Mvc.Rendering;

using UpakModelsLibrary.Models;

namespace UpakModelsLibrary.Models.ViewModels
{
	public class ProductVM
	{
		public Product? Product { get; set; }
		public IEnumerable<SelectListItem>? CategorySelectedList { get; set; }
	}
}
