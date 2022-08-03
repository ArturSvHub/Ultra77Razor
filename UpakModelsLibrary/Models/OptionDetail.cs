using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpakModelsLibrary.Models
{
	public class OptionDetail
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public int ProductOptionId { get; set; }
		[ForeignKey("ProductOptionId")]
		public ProductOption? ProductOption { get; set; }
	}
}