using Actio.Common.Auth;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Extension;
using Actio.Common.Mongo;
using Actio.Services.Activities;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Repositories;
using Actio.Services.Identity.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstratction;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Actio.Services.Identity
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
            services.AddEventBus(Configuration);
            services.AddIntegrationServices(Configuration);
            services.AddMongoDB(Configuration);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEncrypter, Encrypter>();
            services.AddTransient<IJwtHandler, JwtHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IIntegrationEventHandler<UserCreated>, UserCreatedIntegrationEventHandler>();
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            //app.UseHttpsRedirection();
            app.UseMvc();
            ConfigureEventBus(app);

        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreated, IIntegrationEventHandler<UserCreated>>();
        }
    }
}
