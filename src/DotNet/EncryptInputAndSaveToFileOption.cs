using DotNet.Helpers.Core;
using EasyConsole;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp
{
   
    class EncryptInputAndSaveToFileOption
    {
        IDataProtectionProvider dependency;
        ILogger<EncryptInputAndSaveToFileOption> logger;
        IConfigurationRoot configRoot;
        public EncryptInputAndSaveToFileOption(IDataProtectionProvider dep,IConfigurationRoot configRoot, ILogger<EncryptInputAndSaveToFileOption> logger)
        {
            dependency = dep;
            this.logger = logger;
            this.configRoot = configRoot;
        }
        async internal Task Execute()

        {
            logger.LogTrace($"{nameof(EncryptInputAndSaveToFileOption)} : Start");
            string unprotectedText = Input.ReadString("Enter the string to protect");
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedText);
            var protectedBytes = dependency.CreateProtector("").Protect(unprotectedBytes);
            using (Stream s = new MemoryStream(protectedBytes))
            {
                using (FileStream destination = File.Open(configRoot["FilePath"],FileMode.Truncate,FileAccess.Write,FileShare.None)) 
                {
                    await s.CopyToAsync(destination);
                }
            }
            logger.LogInformation($"{nameof(EncryptInputAndSaveToFileOption)} : encrypted {unprotectedText} and saved to '{configRoot["FilePath"]}'");
        }
    }
}