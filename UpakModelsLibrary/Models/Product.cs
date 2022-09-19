using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpakModelsLibrary.Models
{ 
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name ="Название товара")]
		public string? Name { get; set; }
		[Display(Name = "Описание товара")]
		public string? Description { get; set; }
		[Display(Name = "Короткое описание")]
		public string? ShortDesc { get; set; }
		public byte[]? Image { get; set; }
		[Display(Name = "Штрихкод")]
		public string? Barcode { get; set; }
		[Display(Name = "Артикль товара")]
		public int? Article { get; set; }
		[Display(Name = "Создано")]
		public DateTime CreatedDateTime { get; set; }
		[Range(1, 5000000)]
		[Display(Name = "закупочная цена")]
		public double? PurchasePrice { get; set; }
		[Range(1, 50000000)]
		[Display(Name = "Розничная цена")]
		public double? RetailPrice { get; set; }
		[Display(Name = "Категория товара")]
		public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
		[Required]
        public virtual Category? Category { get; set; }
		[NotMapped]
		[Range(1,10000)]
        public int TempCount { get; set; }
		[NotMapped]
		public bool IsChecked { get; set; }
		public List<ProductOption>? ProductOptions { get; set; } = new();
		public int? PageId { get; set; }
		[ForeignKey("PageId")]
		public Page? Page { get; set; }
	}
}
