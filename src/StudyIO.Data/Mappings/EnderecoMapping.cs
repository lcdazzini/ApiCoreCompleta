using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyIO.Business.Models;

namespace StudyIO.Data.Mappings
{
	public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
	{
		public void Configure(EntityTypeBuilder<Endereco> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Logradouro)
				.IsRequired()
				.HasColumnType("nvarchar(200)");

			builder.Property(p => p.Numero)
				.IsRequired()
				.HasColumnType("int");

			builder.Property(p => p.Cep)
				.IsRequired()
				.HasColumnType("nvarchar(8)");

			builder.Property(p => p.Complemento)
				.HasColumnType("nvarchar(200)");

			builder.Property(p => p.Bairro)
				.IsRequired()
				.HasColumnType("nvarchar(100)");

			builder.Property(p => p.Cidade)
				.IsRequired()
				.HasColumnType("nvarchar(100)");

			builder.Property(p => p.Estado)
				.IsRequired()
				.HasColumnType("nvarchar(50)");

			builder.ToTable("Enderecos");

		}
	}
}
