
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Actio.Common.Extension;
using Microsoft.Extensions.Logging;
using bus = EventBusRabbitMQ;
using Actio.Common.Mongo;
using EventBus.Abstratction;
using EventBusRabbitMQ;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Actio.Common.Auth;
using System;
using MediatR;
using System.Reflection;

namespace Actio.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();    
            services.AddJwt(Configuration);
            services.AddIntegrationServices(Configuration);
            services.AddEventBus(Configuration);
            services.AddMongoDB(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            // app.UseAuthentication();
            app.UseMvc();

            ///We can use Ocelot also for Redirection Also
            ///But for our simpler case we will use this approach of Publishing Integration Event for the 
            ///interested api
            //app.UseOcelot();
         
        }

       
    }
}
