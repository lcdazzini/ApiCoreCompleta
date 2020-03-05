using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyIO.Business.Models;

namespace StudyIO.Data.Mappings
{
	public class ProdutoMapping : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Nome)
				.IsRequired()
				.HasColumnType("nvarchar(200)");

			builder.Property(p => p.Descricao)
				.IsRequired()
				.HasColumnType("nvarchar(200)");

			builder.Property(p => p.Imagem)
				.IsRequired()
				.HasColumnType("nvarchar(100)");

			builder.Property(p => p.Nome)
				.IsRequired()
				.HasColumnType("nvarchar(200)");

			builder.Property(e => e.DtCadastro)
			   .HasColumnType("datetime")
			   .HasDefaultValueSql("GETDATE()")
			   .IsRequired();

			builder.Property(e => e.Ativo)
				.HasColumnType("bit")
				.HasDefaultValue(false)
				.IsRequired();

			builder.Property(e => e.Valor)
				.HasColumnType("decimal(18,4)")
				.HasDefaultValue(0)
				.IsRequired();

			builder.ToTable("Produtos");

		}
	}
}
