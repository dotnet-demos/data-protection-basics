using EasyConsole;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        public EncryptInputAndSaveToFileOption encryptOption { get; init; }
        public DecryptFileContentsOption decryptFileContentsOption { get; init; }
        public MenuService(EncryptInputAndSaveToFileOption opt1, DecryptFileContentsOption decryptFileContentsOption)
        {
            encryptOption = opt1;
            this.decryptFileContentsOption = decryptFileContentsOption;

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var menu = new Menu()
                .Add(@"Encrypt and save to c:\temp\encrypted.dat", async (token) => await encryptOption.Execute())
                .Add(@"Load from c:\temp\encrypted.dat and decrypt",async(token)=>await decryptFileContentsOption.Execute())
                .AddSync("Exit", () => Environment.Exit(0));
            await menu.Display(CancellationToken.None);
            await base.StartAsync(stoppingToken);
        }
    }
}