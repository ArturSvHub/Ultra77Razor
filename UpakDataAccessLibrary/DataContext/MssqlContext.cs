using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using UpakModelsLibrary.Models;

namespace UpakDataAccessLibrary.DataContext
{
	public class MssqlContext:IdentityDbContext
	{
		public MssqlContext()
		{
		}

		public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
		{

		}
        public DbSet<Address>? Addresses { get; set; }
		public DbSet<Category>? Categories { get; set; }
		public DbSet<Product>? Products { get; set; }
		public DbSet<UltrapackUser>? UltrapackUsers { get; set; }
		public DbSet<OrderHeader>? OrderHeaders { get; set; }
		public DbSet<OrderDetails>? OrderDetails { get; set; }
		public DbSet<ProductOption>? ProductOptions { get; set; }
		public DbSet<OptionDetail>? OptionDetails { get; set; }
	}
}
