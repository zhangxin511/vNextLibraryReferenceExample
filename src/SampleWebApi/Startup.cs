using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using HttpEx;
using Library.WebApi;

namespace SampleWebApi
{
    public class Startup
    {
		public const string DefaultRouteName = "api";

		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
			JsonUtility.ConfigureDefaults(services);
			IoCSettings.RegisterTypes(services);
        }

        public void Configure(IApplicationBuilder app)
        {
			//app.UseWelcomePage();
			app.UseStaticFiles();
			app.UseMvc(routes => routes.MapRoute("default","{controller=Version}/{action=Get}/{id?}"));
			//app.Run(async (context) =>
   //         {
   //             await context.Response.WriteAsync("Hello World!");
   //         });
        }
    }
}
