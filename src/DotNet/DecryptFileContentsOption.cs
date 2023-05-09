using EasyConsole;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class DecryptFileContentsOption
    {
        IDataProtectionProvider dependency;
        ILogger<EncryptInputAndSaveToFileOption> logger;
        IConfigurationRoot configRoot;

        public DecryptFileContentsOption(IDataProtectionProvider dep, IConfigurationRoot configRoot, ILogger<EncryptInputAndSaveToFileOption> logger)
        {
            dependency = dep;
            this.logger = logger;
            this.configRoot = configRoot;
        }
        async internal Task Execute()
        {
            logger.LogDebug($"{nameof(DecryptFileContentsOption)} : Start");
            var protectedBytes = File.ReadAllBytes(configRoot["FilePath"]);
            var unprotectedBytes = dependency.CreateProtector("").Unprotect(protectedBytes);
            string unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);
            logger.LogInformation($"{nameof(DecryptFileContentsOption)} : Decrypted from '{configRoot["FilePath"]}' and contents are {unprotectedText}.");
            await Task.Delay(1); //Just to keep VisualStudio happy for async
        }
    }
}