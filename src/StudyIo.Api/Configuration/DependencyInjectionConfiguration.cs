﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StudyIo.Api.Extensions;
using StudyIO.Business.Interfaces;
using StudyIO.Business.Notifications;
using StudyIO.Business.Services;
using StudyIO.Data.Context;
using StudyIO.Data.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

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

			// User do sistema
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IUser, AspNetUser>();

			// Swagger
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			return services;
		}
	}
}
