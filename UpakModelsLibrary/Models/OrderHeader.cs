using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpakModelsLibrary.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }
		public string? UltrapackUserId { get; set; }
		[ForeignKey("UltrapackUserId")]
		public UltrapackUser? UltrapackUser { get; set; }
		public DateTime? OrderDate { get; set; }
		public string? PhoneNumber { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }

	}
}
