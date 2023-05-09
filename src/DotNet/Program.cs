using System.Threading.Tasks;
using DotNet.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        async static Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddHostedService<MenuService>();
                    services.AddSingleton< DecryptFileContentsOption>();
                    services.AddSingleton<EncryptInputAndSaveToFileOption>();

                    services.AddDataProtection();
                    services.AddSingleton(new ConfigurationBuilder()
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            {"FilePath",@"c:\temp\encrypted.dat" }
                        })
                        .Build());
                })
                //.UseConsoleLifetime() // This may be used when running inside container. But we dont really run an interative menu program in container.
                .Build()
                .RunAsync();
    }
    class Configurations
    {

    }
}