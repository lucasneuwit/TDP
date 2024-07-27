namespace TDP.Models.Encryption;

public class EncryptionOptions
{
    public string Key { get; set; } = null!;

    public string InitializationVector { get; set; } = null!;
}