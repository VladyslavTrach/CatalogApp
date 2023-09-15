using CatalogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApp.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		public DbSet<CatalogModel> Catalogs { get; set; }
	}
}
