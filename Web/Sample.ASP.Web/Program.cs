using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Sample.ASP.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment;
                    var envName = env.EnvironmentName;
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
