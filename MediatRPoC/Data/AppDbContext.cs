using MediatRPoC.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRPoC.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Customer> Users { get; set; }
		public DbSet<Order> Orders { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
