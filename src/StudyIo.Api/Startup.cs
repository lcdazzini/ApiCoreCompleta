using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyIo.Api.Configuration;
using StudyIo.Api.Extensions;
using StudyIO.Data.Context;

namespace StudyIo.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }


		public Startup(IHostEnvironment hostEnvironment)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(hostEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings{hostEnvironment.EnvironmentName}.json", true, true)
				.AddEnvironmentVariables();

			if (hostEnvironment.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

			Configuration = builder.Build();
		}


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<StudyIODbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddIdentityConfiguration(Configuration);

			services.AddAutoMapper(typeof(Startup));

			services.WebApiConfig();

			services.AddSwaggerConfig();

			services.AddLoggingConfiguration(Configuration);

			services.ResolveDependencies();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseCors("Development");
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseCors("Production");
				app.UseHsts();
			}

			app.UseAuthentication();

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseConfiguration();

			app.UseSwaggerConfig(provider);

			app.UseLoggingConfiguration();

		
		}
	}
}
