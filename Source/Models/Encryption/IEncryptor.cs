using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TDP.Models.Encryption;

public interface IEncryptor
{
    void Encrypt(string password);

    void Decrypt();
}

public class Encryptor : IEncryptor
{
    public void Encrypt(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }

    public void Decrypt()
    {
        throw new NotImplementedException();
    }
}