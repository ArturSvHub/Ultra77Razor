using System.ComponentModel.DataAnnotations;

namespace UpakModelsLibrary.Models
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		public int PostalCode { get; set; }
		[Required]
		public string? Country { get; set; }
		[Required]
		public string? State { get; set; }
		[Required]
		public string? City { get; set; }
		[Required]
		public string? Street { get; set; }
		[Required]
		public string? HouseNumber { get; set; }
		public int? BuildingNumber { get; set; }
		public int? BuildingPartNumber { get; set; }
		public int? AppartmentNumber { get; set; }
		public int? OfficeNumber { get; set; }
		public int? RoomNumber { get; set; }
	}
}