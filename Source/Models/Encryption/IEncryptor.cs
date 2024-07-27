using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TDP.Models.Encryption;

public interface IEncryptor
{
    Task<string> Encrypt(string password);

    string Decrypt(string hashedPassword);
}