using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using RawRabbit;
using System;


namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _WebHost;

        public ServiceHost(IWebHost webhost)
        {
            _WebHost = webhost;
        }

        public void Run() => _WebHost.Run();

        public static HostBuilder Create<TStartUp>(string[] args) where TStartUp : class
        {
            Console.Title = typeof(TStartUp).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)                
                .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseStartup<TStartUp>()
                .UseKestrel();

            return new HostBuilder(host.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _WebHost;
            private IBusClient _BusClient;

            public HostBuilder(IWebHost webhost)
            {
                _WebHost = webhost;
            }

            public BusBuilder UseRabbitMQ()
            {
                _BusClient = (IBusClient)_WebHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(_WebHost, _BusClient);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_WebHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _WebHost;

            private IBusClient _BusClient;

            public BusBuilder(IWebHost webhost, IBusClient busClient)
            {
                _WebHost = webhost;
                _BusClient = busClient;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_WebHost);
            }
        }
    }
}

