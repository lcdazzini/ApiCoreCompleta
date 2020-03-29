using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudyIo.Api.Extensions;
using System;

namespace StudyIo.Api.Configuration
{
	public static class LoggerConfiguration
	{
		public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddElmahIo(o =>
			{
				o.ApiKey = "3b94bbbcb41e41e0991309dcfe373e8a";
				o.LogId = new Guid("db7beab9-7923-4003-8d2c-ab77948f1868");
			});

			services.AddLogging(builder =>
			{
				builder.AddElmahIo(o =>
				{
					o.ApiKey = "3b94bbbcb41e41e0991309dcfe373e8a";
					o.LogId = new Guid("db7beab9-7923-4003-8d2c-ab77948f1868");
				});
				builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
			});

			services.AddHealthChecks()
				.AddElmahIoPublisher(options =>
				{
					options.ApiKey = "3b94bbbcb41e41e0991309dcfe373e8a"; 
					options.LogId = new Guid("db7beab9-7923-4003-8d2c-ab77948f1868");
					options.Application = "Api Fornecedores";
					options.HeartbeatId = Guid.NewGuid().ToString();
				})
				.AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSql")
				.AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")));

			services.AddHealthChecksUI();


			return services;
		}

		public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
		{
			app.UseElmahIo();

			app.UseHealthChecks("/api/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});

			app.UseHealthChecksUI(options =>
			{
				options.UIPath = "/api/healthcheck";
			});

			return app;
		}
	}
}
