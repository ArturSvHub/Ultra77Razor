using System.ComponentModel.DataAnnotations;

namespace UpakModelsLibrary.Models
{
	public class ProductOption
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Description { get; set; }
		public List<Product>? Products { get; set; } = new ();
		public List<OptionDetail>? OptionDetails { get; set; } = new();

	}
}