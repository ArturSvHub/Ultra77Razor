using UpakModelsLibrary.Models;

namespace UpakModelsLibrary.Models.ViewModels
{
	public class CategoryVM
	{
		public Category? Category { get; set; }
		public IEnumerable<Category>? CategoriesForSelect { get; set; }
	}
}
