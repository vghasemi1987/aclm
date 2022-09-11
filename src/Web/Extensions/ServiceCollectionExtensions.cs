using DomainEntities.ApplicationUserAggregate;
using FluentValidation.AspNetCore;
using Infrastructure.Data.Commons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Web.Extensions.Attributes;

namespace Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void Build(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("Permission", policyBuilder =>
				{
					policyBuilder.Requirements.Add(new PermissionAuthorizationRequirement());
				});
			});
			services.AddAntiforgery(opts => opts.Cookie.Name = "_gui");
		}
		public static void AddServiceValidation(this IServiceCollection services)
		{
		}
		public static void AddCustomizedMvc(this IServiceCollection services)
		{
			services.AddControllersWithViews()
				.AddRazorRuntimeCompilation()
					.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
					.AddJsonOptions(options =>
						options.JsonSerializerOptions.PropertyNamingPolicy = null)
					.AddControllersAsServices()
					.AddNewtonsoftJson(options =>
					{
						options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
						//generate CamelCase Property Contract Resolver
						options.UseMemberCasing();
					});


			//services.AddMvc(options =>
			//{
			//	//options.Filters.Add<UserLogMessageAttribute>();

			//})
			//	.AddRazorRuntimeCompilation()
			//	.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
			//	.AddJsonOptions(options =>
			//		options.JsonSerializerOptions.PropertyNamingPolicy = null)
			//	.AddControllersAsServices()
			//	.AddNewtonsoftJson(options =>
			//	{
			//		options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			//		//generate CamelCase Property Contract Resolver
			//		options.UseMemberCasing();
			//	});

		}
		public static void AddCustomizedCookie(this IServiceCollection services)
		{
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromHours(24);
				options.LoginPath = "/usermanagement/signin";
				options.LogoutPath = "/usermanagement/signout";
				options.Cookie.Name = "_cnf";
				options.AccessDeniedPath = new PathString("/error/code/403");
			});
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
				options.ConsentCookie.Name = "cookieconsent_status";
			});

			services.AddSession(options =>
			{
				options.Cookie.IsEssential = true;
				//options.Cookie.Expiration = TimeSpan.FromMinutes(1);
				options.IdleTimeout = TimeSpan.FromMinutes(1);
			});
		}
		public static void AddCustomizedIdentity(this IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequiredLength = 5;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.User.RequireUniqueEmail = false;
				})
				.AddEntityFrameworkStores<ServerAccessibilityMonitorContext>()
				.AddDefaultTokenProviders();


			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(o => o.LoginPath = new PathString("/usermanagement/signin"));

			//services.AddScoped<IUserLogMessageRepository, UserLogMessageRepository>();
			//services.AddScoped<UserLogMessageViewModel>();

			services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory>();

		}
		public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ServerAccessibilityMonitorContext>(c =>
			{
				c.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
									 sqlServerOptionsAction: sqlOptions =>
									 {
										 sqlOptions.CommandTimeout(commandTimeout: 1000);
										 sqlOptions.MigrationsAssembly(typeof(ServerAccessibilityMonitorContext).GetTypeInfo().Assembly.GetName().Name);
										 sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
									 });

			});


			return services;
		}
	}
}