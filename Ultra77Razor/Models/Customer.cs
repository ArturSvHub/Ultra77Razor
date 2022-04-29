using System.ComponentModel.DataAnnotations;

namespace Ultra77Razor.Models
{
	public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public List<Order> Orders { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
