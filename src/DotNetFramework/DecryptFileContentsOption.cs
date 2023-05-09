using EasyConsole;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework
{
    class DecryptFileContentsOption
    {
        public DecryptFileContentsOption()
        {
        }
        async internal Task Execute()
        {
            Output.WriteLine($"{nameof(DecryptFileContentsOption)} : Start");
            var protectedBytes = File.ReadAllBytes(Configurations.FilePath);
            var unprotectedBytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
            string unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);
            Output.WriteLine($"{nameof(DecryptFileContentsOption)} : Decrypted from '{Configurations.FilePath}' and contents are {unprotectedText}.");
            await Task.Delay(1); //Just to keep VisualStudio happy for async
        }
    }
}
