using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();// adding custom services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();//this must be before UseStaticFiles in order to make index.html default
            app.UseStaticFiles();//must add this so that we can see index.html
            

            //register custom middleware
            app.Use(next =>
            {
                return async context =>
                {
                    logger.LogInformation("Request incoming");
                    if (context.Request.Path.StartsWithSegments("/xxx"))
                    {
                        await context.Response.WriteAsync("Custom middleware for xxx path");
                        logger.LogInformation("Request handled by this middleware");//this will not be passed to next middleware
                    }
                    else
                    {
                        await next(context);
                        logger.LogInformation("Request outcomming");//this code is executed after next middelware returns response
                    }
                };
            });

            app.UseWelcomePage(new WelcomePageOptions(){Path = "/UseWelcomePageOnlyForThisPath"}); // register middleware that will show welcome page only for given path

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
                var greeting = configuration["Greeting"];//reading value from appsettings.json
                await context.Response.WriteAsync($"<p>{greeting}</p>");
                await context.Response.WriteAsync($"<p>{greeter.GetMessage()}</p>");//needs to configure this dependancy injection
            });
        }
    }
}
