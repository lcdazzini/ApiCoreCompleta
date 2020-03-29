using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Notifications;
using System;
using System.Linq;

namespace StudyIo.Api.Controllers
{
	[ApiController]
	public abstract class MainController : ControllerBase
	{
		private readonly INotificador _notificador;
		public readonly IUser _appUser;

		protected Guid UsuarioId { get; set; }
		protected bool UsuarioAutenticado { get; set; }

		public MainController(
			INotificador notificador,
			IUser appUser)
		{
			_notificador = notificador;
			_appUser = appUser;

			if (appUser.IsAuthenticated())
			{
				UsuarioId = appUser.GetUserId();
				UsuarioAutenticado = true;
			}
		}

		protected bool OperacaoValida()
		{
			return !_notificador.TemNotificacao();
		}

		protected ActionResult CustomResponse(object result = null)
		{
			if (OperacaoValida())
			{
				return Ok(new
				{
					success = true,
					data = result
				});

			}

			return BadRequest(new
			{
				sucess = false,
				errors = _notificador.ObterNotificacoes().Select(m => m.Mensagem)
			});

		}

		protected ActionResult CustomResponse(ModelStateDictionary modelState)
		{
			if (!modelState.IsValid) NotificaErroModelInvalida(modelState);
			return CustomResponse();
		}

		// validação de notificação de erro
		protected void NotificaErroModelInvalida(ModelStateDictionary modelState)
		{
			var errors = modelState.Values.SelectMany(e => e.Errors);
			foreach (var error in errors)
			{
				var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
				NotificarErro(errorMsg);
			}
		}

		protected void NotificarErro(string mensagem)
		{
			_notificador.Handle(new Notificacao(mensagem));
		}

		// validaação de modelstate

		// validação de operação de negócios


	}
}
