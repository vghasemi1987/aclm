using System;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseCustomizedIdentity(this IApplicationBuilder app)
		{
			app.UseAuthentication();
		}
		public static void UseCustomizedResponseCompression(this IApplicationBuilder app)
		{
			app.UseResponseCompression();
		}
		public static void UseCustomizedMvc(this IApplicationBuilder app)
		{
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(routes =>
			{
				//routes.Routes.Add(new UrlSlugRoute(routes.DefaultHandler));
				routes.MapControllerRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
		public static IApplicationBuilder UseCustomizedRequestLocalization(this IApplicationBuilder app)
		{
			var defaultCulture = new CultureInfo("en-US");
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture),
				//SupportedCultures = supportedCultures,
				//SupportedUICultures = supportedCultures
			});
			return app;
		}
		public static void UseCustomizedStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseStaticFiles(new StaticFileOptions
				{
					OnPrepareResponse = context =>
					{
						var headers = context.Context.Response.GetTypedHeaders();
						headers.CacheControl = new CacheControlHeaderValue
						{
							NoCache = true,
							NoStore = true,
							MaxAge = TimeSpan.FromDays(-1)
						};
					}
				});
			}
			else
			{
				app.UseStaticFiles(new StaticFileOptions
				{
					OnPrepareResponse = context =>
					{
						var headers = context.Context.Response.GetTypedHeaders();
						headers.CacheControl = new CacheControlHeaderValue
						{
							Public = true,
							MaxAge = TimeSpan.FromDays(60)
						};
					}
				});
			}
		}
	}
}