using StudyIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace StudyIO.Business.Interfaces
{
	public interface IEnderecoRepository : IRepository<Endereco>
	{
		Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
	}
}
