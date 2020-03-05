using Microsoft.EntityFrameworkCore;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Models;
using StudyIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyIO.Data.Repository
{
	public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
	{
		public FornecedorRepository(StudyIODbContext context) : base(context) { }

		public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
		{
			return await Db.Fornecedores.AsNoTracking()
				.Include(f => f.Endereco)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
		{
			return await Db.Fornecedores.AsNoTracking()
				.Include(f => f.Produtos)
				.Include(f => f.Endereco)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
