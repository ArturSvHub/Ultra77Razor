using Microsoft.EntityFrameworkCore;

using Ultra77Razor.Models;

namespace Ultra77Razor.DataContext
{
	public class MssqlContext:DbContext
	{
		public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
		{

		}
	public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		
	}
}
