using System.ComponentModel.DataAnnotations;

namespace Ultra77Razor.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Description { get; set; }
		[Required]
		public string Name { get; set; }
		public string SubCategoryName { get; set; }
		public byte[] Image { get; set; }
		public List<Product> Products { get; set; }
	}
}
