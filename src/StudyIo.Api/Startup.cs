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
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

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
