namespace TDP.Models.ViewModels;

public class RegistrationViewModel
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}