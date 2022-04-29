using System.ComponentModel.DataAnnotations;

namespace Ultra77Razor.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDesc { get; set; }
		public byte[] Image { get; set; }
		public string Barcode { get; set; }
		public int Article { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public decimal PurchasePrice { get; set; }
		public decimal RetailPrice { get; set; }
		public Category Category { get; set; }
	}
}
