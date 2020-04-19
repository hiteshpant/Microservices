using Actio.Common.Commands;
using Actio.Common.Services;

namespace Actio.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                  .UseRabbitMQ()                 
                  .Build()
                  .Run();
        }       
    }
}
