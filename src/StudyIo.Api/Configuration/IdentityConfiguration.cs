﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyIo.Api.Data;
using StudyIo.Api.Extensions;

namespace StudyIo.Api.Configuration
{
	public static class IdentityConfiguration
	{
		public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>()
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddErrorDescriber<IdentityMensagensPortugues>()
				.AddDefaultTokenProviders();

			return services;
		}
	}
}
