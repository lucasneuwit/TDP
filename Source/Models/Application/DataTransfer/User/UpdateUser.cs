namespace TDP.Models.Application;

public record UpdateUser(
    Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string EmailAddress,
    byte[]? ProfilePicture = default)
{
    public Guid Id { get; set; } = Id;

    public string Username { get; set; } = Username;

    public string FirstName { get; set; } = FirstName;

    public string LastName { get; set; } = LastName;

    public string EmailAddress { get; set; } = EmailAddress;

    public byte[]? ProfilePicture { get; set; } = ProfilePicture;
}