using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyIO.Business.Models;

namespace StudyIO.Data.Mappings
{
	public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
	{
		public void Configure(EntityTypeBuilder<Fornecedor> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Nome)
				.IsRequired()
				.HasColumnType("nvarchar(200)");

			builder.Property(p => p.Documento)
				.IsRequired()
				.HasColumnType("nvarchar(14)");

			builder.Property(e => e.Ativo)
				.HasColumnType("bit")
				.HasDefaultValue(false)
				.IsRequired();

			// 1 para 1 => Fornecedor : Endereco
			builder.HasOne(f => f.Endereco)
				.WithOne(e => e.Fornecedor);

			// 1 : N => Fornecedor : Produtos
			builder.HasMany(f => f.Produtos)
				.WithOne(p => p.Fornecedor)
				.HasForeignKey(p => p.FornecedorId);


			builder.ToTable("Fornecedores");

		}
	}
}
