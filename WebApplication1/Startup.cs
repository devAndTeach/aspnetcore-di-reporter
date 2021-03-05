using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Startup
    {


        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(services);
            _services = services;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            // add this if you want to add this for a particular path in an existing app
            app.Map("/availableObjects", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                int count = 0;
                sb.Append("<html>");
                sb.Append("<head><title> SystemDetails </title>");
                sb.Append("<link rel =\"stylesheet\" type = \"text/css\" href = \"/css/styles.css\" >");
                sb.Append("</head > ");
                sb.Append("<body>");

                sb.Append("<h1>All Objects</h1>");
                sb.Append("<table id=\"services\"><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                    ++count;
                }
                sb.Append("</tbody></table>");
                sb.Append($"<p>Total registered types: {count}</p>");
                sb.Append("</body>");
                sb.Append("</html>");
                await context.Response.WriteAsync(sb.ToString());
            }));


             app.UseStaticFiles();
            app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });



        }

        


    }
}
