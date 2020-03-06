﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudyIO.Data.Context;

namespace StudyIO.Data.Migrations
{
    [DbContext(typeof(StudyIODbContext))]
    [Migration("20200218004157_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudyIO.Business.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("StudyIO.Business.Models.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TipoFornecedor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("StudyIO.Business.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DtCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,4)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("StudyIO.Business.Models.Endereco", b =>
                {
                    b.HasOne("StudyIO.Business.Models.Fornecedor", "Fornecedor")
                        .WithOne("Endereco")
                        .HasForeignKey("StudyIO.Business.Models.Endereco", "FornecedorId")
                        .IsRequired();
                });

            modelBuilder.Entity("StudyIO.Business.Models.Produto", b =>
                {
                    b.HasOne("StudyIO.Business.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}