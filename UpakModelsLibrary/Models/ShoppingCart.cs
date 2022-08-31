namespace UpakModelsLibrary.Models
{
	public class ShoppingCart
	{
		public ShoppingCart()
		{
			ProductOptions = new();
		}
		public int? ProductId { get; set; }
		public int? TempCount{ get; set; }
		public Dictionary<string,string>? ProductOptions{ get; set; }
	}
}
