using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Service
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
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((ctx, cfg) =>
                {

                    cfg.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                        .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                        .WriteTo.GrafanaLoki(ctx.Configuration["Loki"]);

                    if (ctx.HostingEnvironment.IsDevelopment())
                        cfg.WriteTo.Console(new RenderedCompactJsonFormatter());
                });
    }
}
