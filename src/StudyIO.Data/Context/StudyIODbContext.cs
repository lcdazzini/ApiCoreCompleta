using Microsoft.EntityFrameworkCore;
using StudyIO.Business.Models;
using System.Linq;

namespace StudyIO.Data.Context
{
	public class StudyIODbContext : DbContext
	{
		public StudyIODbContext(DbContextOptions options) : base(options) { }


		public DbSet<Endereco> Enderecos { get; set; }

		public DbSet<Fornecedor> Fornecedores { get; set; }

		public DbSet<Produto> Produtos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// cadastra com nvarchar 100 se não for setado no mappings
			foreach (var property in modelBuilder.Model.GetEntityTypes()
					.SelectMany(e => e.GetProperties()
						.Where(p => p.ClrType == typeof(string))))
				property.SetColumnType("nvarchar(100)");


			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudyIODbContext).Assembly);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes()
				.SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

			base.OnModelCreating(modelBuilder);
		}

	}
}
