using Microsoft.EntityFrameworkCore;

using Ultra77Razor.Models;

namespace Ultra77Razor.DataContext
{
	public class PostgresContext:DbContext
	{
		public DbSet<Address> Addresses{ get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.
			UseNpgsql(@"Host=194.67.116.87;Database=upak;Username=root;Password=Vedaniesql2022.");
		public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
		{
			
		}
	}
}
