using AutoMapper;
using StudyIo.Api.ViewModels;
using StudyIO.Business.Models;

namespace StudyIo.Api.Configuration
{
	public class AutomapperConfiguration : Profile
	{
		public AutomapperConfiguration()
		{
			CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
			CreateMap<Produto, ProdutoViewModel>().ReverseMap();
			CreateMap<Produto, ProdutoImagemViewModel>().ReverseMap();
		}
	}
}
