using Microsoft.EntityFrameworkCore;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Models;
using StudyIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyIO.Data.Repository
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{

		public ProdutoRepository(StudyIODbContext context) : base(context) { }
		
		public async Task<Produto> ObterProdutoFornecedor(Guid id)
		{
			return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
		{
			return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor).ToListAsync();
		}

		public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
		{
			return await Buscar(p => p.FornecedorId == fornecedorId);
		}
	}
}
