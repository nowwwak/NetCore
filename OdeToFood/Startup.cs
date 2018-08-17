using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();// adding custom services as singleton
            //services.AddScoped // life time is for HTTP request
            //services.AddSingleton<IRestaurantData,InMemoryRestaurantData>(); // this should be scoped, but for testing I want to have same datasource
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRestaurantData,SqlRestaurantData>();
            services.AddMvc();// needed for mvc 
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

            //app.UseMvcWithDefaultRoute(); // this is mvc with default route
            app.UseMvc(ConfigureRoutes); // this is mvc with configured route

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
                context.Response.ContentType = "text/plain";//we can see this explicity to be sure that text is rendered ok, without that some browsers may render text without spaces
                await context.Response.WriteAsync("Hello World!");
                var greeting = configuration["Greeting"];//reading value from appsettings.json
                await context.Response.WriteAsync($"<p>{greeting}</p>");
                await context.Response.WriteAsync($"<p>{greeter.GetMessage()}</p>");//needs to configure this dependancy injection
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index
            // /Home/Index/4
            // we set defult values for controller and action
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
