using Autofac;
using Autofac.Extensions.DependencyInjection;
using DomainEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Web.ActionFilters;
using Web.Extensions;
using Web.Extensions.AutofacServiceResolver;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace Web
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

			services.AddSingleton(c =>
			{
				ConfigurationDto result = new ConfigurationDto(_configuration.GetConnectionString("DatabaseConnection"));
				return result;
			});

			//services.AddScoped<IUserLogMessageRepository, UserLogMessageRepository>();
			services.AddTransient<UserLogMessageAttribute>();

			//services.AddDbContext<ServerAccessibilityMonitorContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection")));
			services.AddCustomizedDataStore(_configuration);
			services.AddCustomizedIdentity();
			services.AddCustomizedCookie();

			services.Configure<FormOptions>(x =>
			{
				x.MultipartBodyLengthLimit = 1073741824;
				x.ValueLengthLimit = 1073741824;
				x.MultipartHeadersLengthLimit = 1073741824;
			});

			services.ConfigureApplicationCookie(option =>
			{
				option.ExpireTimeSpan = TimeSpan.FromHours(24);
			});
			services.AddAutoMapper(typeof(Startup));
			services.AddCustomizedMvc();

			//services.AddScoped<IUserLogMessageRepository, UserLogMessageRepository>();
			//services.AddScoped<UserLogMessageViewModel>();
			//services.AddScoped<UserLogMessageAttribute>();

			services.Build();
			services.AddServiceValidation();

			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

			services.Configure<FormOptions>(x =>
			{
				x.MultipartBodyLengthLimit = int.MaxValue; //209715200;
				x.ValueLengthLimit = int.MaxValue;
				x.MultipartHeadersLengthLimit = int.MaxValue;
			});

			services.AddResponseCompression();

			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationExpanders.Insert(0 , new AppViewLocationExpander());
			});

			var builder = new ContainerBuilder();
			builder.RegisterModule(new RepositoryHandlerModule());
			builder.Populate(services);

			var container = builder.Build();
			//services.Configure<FormOptions>(x =>
			//{
			//    x.MultipartBodyLengthLimit = long.MaxValue;
			//    x.ValueLengthLimit = int.MaxValue;
			//    x.MultipartHeadersLengthLimit = int.MaxValue;
			//});
			return new AutofacServiceProvider(container);


		}

		public void Configure(IApplicationBuilder app , IWebHostEnvironment env , IWebHostEnvironment webHostEnvironment)
		{
			if ( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
				// app.UseDatabaseErrorPage();
				webHostEnvironment.EnvironmentName = Environments.Development;
			}
			else
			{
				app.UseExceptionHandler("/Catalog/Error");
				app.UseStatusCodePagesWithRedirects("~/error/code/{0}");
				app.UseHsts();
			}
			app.Use(async (context , next) =>
			{
				context.Response.Headers.Add("X-Frame-Options" , "DENY"); // This
																		  //context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); // Or this
				await next();
			});
			app.UseSession();


			app.UseCustomizedRequestLocalization();
			app.UseCustomizedStaticFiles(env);
			app.UseCookiePolicy();
			app.UseCustomizedIdentity();
			app.UseCustomizedMvc();
			app.UseCustomizedResponseCompression();
		}
	}
}