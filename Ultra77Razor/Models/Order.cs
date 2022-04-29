using System.ComponentModel.DataAnnotations;

namespace Ultra77Razor.Models
{
	public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public List<Product> Products { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Customer Customer { get; set; }
    }
}
