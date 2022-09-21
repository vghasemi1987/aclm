﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Run();
		}

		public static IWebHost CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseIISIntegration()
			.UseKestrel(options =>
			{
				//2-UploadLargFile
				options.Limits.MaxRequestBodySize = null;//1073741824;
				options.Limits.MaxRequestHeadersTotalSize = 1073741824;
				options.Limits.MaxRequestBufferSize = 1073741824;
				options.Limits.RequestHeadersTimeout = TimeSpan.FromHours(10);
				options.Limits.KeepAliveTimeout = TimeSpan.FromHours(10);
				options.AddServerHeader = false;
			})
			.UseStartup<Startup>()
			.UseIISIntegration()
			.Build();
	}

	#region[Comment]
	//public class Program
	//{
	//    public static void Main(string[] args)
	//    {
	//        var host = CreateWebHostBuilder(args)
	//            .ConfigureAppConfiguration(SetupConfiguration)
	//            .ConfigureLogging(loggingBuilder => loggingBuilder.AddSerilog())
	//            .UseKestrel(options =>
	//            {
	//                options.Limits.MaxRequestBodySize = 209715200;
	//            })
	//            .Build();
	//        //StiLicense.Key =
	//        //    "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHm4lqN+V3/fvJECXRrqdoEPZqYyFXh3g3K9oCDFvgzMl4c9KudQQwMJ6nO6w7rHz59BhwYgDE0QzKtjY2WxEejTNewbNDXY492M2mDsK1Hb4t6MoFYGbSoID0gow3VC5cFhvcOKVoagiHq6/4iqFc3RS7nZQBp95+WB6N1fR//H7OBlvfuBKldvC1pJh6pTW/7HRdOclErt/EGwivx9SpuDNabWWBfIZJBdJsvKjpoIsDQGibWk8Y8F9V1mCLf88FurWwEZ9WzXePbJn+wfabHM+a7pqDAXhsaW33UEW6vQ3kgLrVjbcHWdhtYbp5j6ZeIMLBCW2LXiCZpzy3N3XqjlroV/s7KRZGeZMvRrLWhcM8YJ6sBTMUyQrF/Fk3d5f0WbAf7+opIA5rhxY+qgwWcE42S+HOOcMgb2IWtj5ycC/GXZ4o9qtk2WWJzCC+ygckXK7iI5pzhzScGmLCBrFnjyE5EP65FViVDSCcNl6hUFw7wwRtcQq5pnOu6wHRXsSpwNPufWaRqE4gd7hbrQoPqki4BLOYgGOQjZ3kJdc7MG47nVT62z1ScLsjH73q4yhABt8h1wmJl0LanKnE6EGkvZPCrDBBf0sZp12sJrxK8FGsPUKrBNZ5f4VuYkdzWS1ed2m5MZNHkDDd3LncYkQnLPh/MYAsnc2ud715njwiM62xIWYk9lIrmCWJU3JVcexRDS3/l7Po9UU5gx2xJKUW1tWOXKQ9GqQZYtn/rl804VMC1/tWr4X2XU1otulny5P8YkZ7BTG6zmkcW6raROxv7xkSUwE0LK2FmIpeBr29Zxn0QUYBtC09J+wVD5y00f/1jMU+k1jQmi1n2tpWgy6CxAHPPrQFhk0FsGXEDg82IELCKncbqkfqgT0QyV262YL2uxrcbcJ1Wrzf4llEX90hMa3oZK5wdQLrCBkXIlXverhGL3I09fR6lcXDMj8A3vrQrhr4bwYcWTH0hyJCPVLYIVxAFLwgV8wk700QOo3z32sSWKnyMHVx3brFB8I7rOPaW2Sc8rBxc1E9EvNcqiTwRd9QA3lIdR2Hpc4jyn5dx8PT6GTNGXNKhoTZDLrGMtYqXw4rVOXfkE8+opF4+/H602ockCI/IY7C65+sxooNgSg+9LpzImUt6GdtrvF8HrvJ0rUW6ZiAmGaQmqdrHfwC3K+QKU4UEWuLU1GPG+PKMrYEbtT/Hei7NPA5sOqHqbzaMsW7zj+enlhuEUeMdlxqfQs1QtTERrzX2HXv/JZ43rASH4ycuGftLR2v3AOHhtBCoYOCszhOiHYGmH0I9lwi8HDRTXjkuoRU1Zxmz9rSIQy35/aypV339OS7p4VydO5LibEiwugzaNHXCZalEPShhcWzTAq6+uCb+jA9ob+Za4yICRdIjgot/E5ZQNZl2VEhFmWR7pYSB7MB24z0ysVf3igAc50/3Lr4d7mQ7SgmtKFoejGOjvb75YOq/pEJGxCo1xIS+jhZklyLuiwmtyDe7Xmpn33fip22qx2iF7FYW71AyuT3tTPn2a7Jc2wXlPCAxB6dqx0eeX4fWA3+RIosUTxTyA7a/SM95pwN9FKYFPZ8TcTqDIq0Ntc57IdX5u1To=";

	//        //using (var scope = host.Services.CreateScope())
	//        //{
	//        //    var services = scope.ServiceProvider;
	//        //    try
	//        //    {
	//        //        //var crmContext = services.GetRequiredService<CrmContext>();
	//        //        //CrmContextSeed.SeedAsync(crmContext).Wait();

	//        //    }
	//        //    catch (Exception)
	//        //    {

	//        //    }
	//        //}
	//        host.Run();
	//    }

	//    private static void SetupConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder configBuilder)
	//    {
	//        var env = hostingContext.HostingEnvironment;
	//        configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	//            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

	//        configBuilder.AddEnvironmentVariables();

	//        var configuration = configBuilder.Build();
	//        Log.Logger = new LoggerConfiguration()
	//            .ReadFrom.Configuration(configuration)
	//            .CreateLogger();
	//    }

	//    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
	//        WebHost.CreateDefaultBuilder(args)
	//            .UseStartup<Startup>()

	//        ;
	//}
	#endregion/[Comment]
}