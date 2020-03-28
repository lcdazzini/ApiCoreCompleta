using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace StudyIo.Api.Configuration
{
	public static class ApiConfiguration
	{
		public static IServiceCollection WebApiConfig(this IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services.Configure<ApiBehaviorOptions>(options =>
				options.SuppressModelStateInvalidFilter = true);

			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			services.AddCors(options =>
			{
				options.AddPolicy("Development",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

			return services;
		}

		public static IApplicationBuilder UseConfiguration(this IApplicationBuilder app)
		{

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
				endpoints.MapControllers());


			return app;
		}
	}
}
