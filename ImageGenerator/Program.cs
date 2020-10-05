using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ImageGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel((context, options) =>
                                              options.Configure(context.Configuration.GetSection("Kestrel")));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
