using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UpakModelsLibrary.Models
{
	public class UltrapackUser:IdentityUser
	{
		public string? FullName { get; set; }
	}
}
