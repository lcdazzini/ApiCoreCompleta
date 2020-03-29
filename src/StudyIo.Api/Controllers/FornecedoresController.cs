using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyIo.Api.Extensions;
using StudyIo.Api.ViewModels;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyIo.Api.Controllers
{
	[Authorize]
	[Route("api/fornecedores")]

	public class FornecedoresController : MainController
	{
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IFornecedorService _fornecedorService;
		private readonly IEnderecoRepository _enderecoRepository;
		private readonly IMapper _mapper;

		public FornecedoresController(
			IFornecedorRepository fornecedorRepository,
			IFornecedorService fornecedorService,
			IEnderecoRepository enderecoRepository,
			IMapper mapper,
			IUser user,
			INotificador notificador) : base(notificador, user)
		{
			_fornecedorRepository = fornecedorRepository;
			_fornecedorService = fornecedorService;
			_enderecoRepository = enderecoRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
		{
			var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

			return fornecedor;
		}


		[HttpGet("{id:guid}")]
		public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
		{
			var fornecedor = await ObterFornecedorProdutosEndereco(id);

			if (fornecedor == null) return NotFound();

			return fornecedor;
		}

		[ClaimsAuthorize("Fornecedor", "Adicionar")]
		[HttpPost("Adicionar")]
		public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
		{
			if (!ModelState.IsValid) return CustomResponse(ModelState);

			await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));

			return CustomResponse(fornecedorViewModel);
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[HttpPut]
		public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
		{
			if (id != fornecedorViewModel.Id)
			{
				NotificarErro("O id informado não é o mesmo executado na query");
				return CustomResponse(fornecedorViewModel);
			}

			if (!ModelState.IsValid) return CustomResponse(ModelState);

			await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

			return CustomResponse(fornecedorViewModel);
		}

		[HttpGet("obter-endereco/{id:guid}")]
		public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
		{
			var enderecoViewModel = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
			return enderecoViewModel;
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[HttpPut("atualizar-endereco/{id:guid}")]
		public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
		{
			if (id != enderecoViewModel.Id)
			{
				NotificarErro("O id informado não é o mesmo executado na query");
				return CustomResponse(enderecoViewModel);
			}

			if (!ModelState.IsValid) return CustomResponse(ModelState);

			await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));

			return CustomResponse(enderecoViewModel);
		}

		[ClaimsAuthorize("Fornecedor", "Excluir")]
		[HttpDelete("{id:guid}")]
		public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null) return NotFound();

			await _fornecedorService.Remover(id);

			return CustomResponse(fornecedorViewModel);
		}


		private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
		}

		private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
		}
	}
}
