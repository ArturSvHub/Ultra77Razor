using System.ComponentModel.DataAnnotations;

namespace Ultra77Razor.Models
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		public int PostalCode { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public int BuildingNumber { get; set; }
		public int BuildingPartNumber { get; set; }
		public int AppartmentNumber { get; set; }
		public int OfficeNumber { get; set; }
		public int RoomNumber { get; set; }
		public Customer Customer { get; set; }
	}
}