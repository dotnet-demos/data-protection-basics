using EasyConsole;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework
{
    class EncryptInputAndSaveToFileOption
    {
        public EncryptInputAndSaveToFileOption()
        {
        }
        async internal Task Execute()
        {
            Output.WriteLine($"{nameof(EncryptInputAndSaveToFileOption)} : Start");
            string unprotectedText = Input.ReadString("Enter the string to protect");
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedText);
            var protectedBytes = ProtectedData.Protect(unprotectedBytes, null, DataProtectionScope.CurrentUser);
            using (Stream s = new MemoryStream(protectedBytes))
            {
                using (FileStream destination = File.OpenWrite(Configurations.FilePath))
                {
                    await s.CopyToAsync(destination);
                }
            }
            Output.WriteLine($"{nameof(EncryptInputAndSaveToFileOption)} : encrypted {unprotectedText} and saved to '{Configurations.FilePath}'");
        }
    }
}
