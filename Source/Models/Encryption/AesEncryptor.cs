using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace TDP.Models.Encryption;

public class AesEncryptor(IOptions<EncryptionOptions> options) : IEncryptor
{
    public async Task<string> Encrypt(string password)
    {
        using var aes = Aes.Create();
        aes.IV = Convert.FromBase64String(options.Value.InitializationVector);
        aes.Key = Convert.FromBase64String(options.Value.Key);

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        {
            await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            await using (var streamWriter = new StreamWriter(cryptoStream))
            {
                await streamWriter.WriteAsync(password);
            
                // Ensure all write operations have completed and data is written
                await streamWriter.FlushAsync();
                await cryptoStream.FlushFinalBlockAsync();
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    public string Decrypt(string hashedPassword)
    {
        var buffer = Convert.FromBase64String(hashedPassword);

        using var aes = Aes.Create();
        aes.IV = Convert.FromBase64String(options.Value.InitializationVector);
        aes.Key = Convert.FromBase64String(options.Value.Key);
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        {
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using (var streamReader = new StreamReader(cryptoStream))
            { 
                return streamReader.ReadToEnd();
            }
        }
    }
}