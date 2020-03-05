using Microsoft.EntityFrameworkCore;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Models;
using StudyIO.Data.Context;
using System;
using System.Threading.Tasks;

namespace StudyIO.Data.Repository
{
	public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
	{
		public EnderecoRepository(StudyIODbContext context) : base(context) { }

		public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
		{
			return await Db.Enderecos.AsNoTracking()
				.FirstOrDefaultAsync(p => p.FornecedorId == fornecedorId);
		}
	}
}
