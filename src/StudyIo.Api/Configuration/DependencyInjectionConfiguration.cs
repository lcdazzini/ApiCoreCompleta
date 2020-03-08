using Microsoft.Extensions.DependencyInjection;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Notifications;
using StudyIO.Business.Services;
using StudyIO.Data.Context;
using StudyIO.Data.Repository;

namespace StudyIo.Api.Configuration
{
	public static class DependencyInjectionConfiguration
	{
		public static IServiceCollection ResolveDependencies(this IServiceCollection services)
		{
			// DbContext
			services.AddScoped<StudyIODbContext>();

			// Repositories
			services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();

			// Services
			services.AddScoped<IFornecedorService, FornecedorService>();
			services.AddScoped<IProdutoService, ProdutoService>();

			// Notificador
			services.AddScoped<INotificador, Notificador>();
			return services;
		}
	}
}
