using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EncryptInputAndSaveToFileOption option1 = new EncryptInputAndSaveToFileOption();
            DecryptFileContentsOption decryptOption = new DecryptFileContentsOption();
            var menu = new Menu()
                .Add(@"Encrypt and save to c:\temp\encrypted.dat", async (token) => await option1.Execute())
                .Add(@"Load from c:\temp\encrypted.dat and decrypt", async (token) => await decryptOption.Execute())
                .AddSync("Exit", () => Environment.Exit(0));
            while (true)
            {
                menu.Display(CancellationToken.None).GetAwaiter().GetResult();
            }
        }
    }
}
