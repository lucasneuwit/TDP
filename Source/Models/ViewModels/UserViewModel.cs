using TDP.Models.Domain;

namespace TDP.Models.ViewModels;

public class UserViewModel
{
    public UserViewModel()
    {
        
    }
    
    public UserViewModel(User user)
    {
        this.Id = user.Id;
        this.Username = user.Username;
        this.FirstName = user.Name;
        this.LastName = user.LastName;
        this.EmailAddress = user.EmailAddress;
        this.IsAdmin = user.IsAdministrator;
        this.ProfilePicture = user.ProfilePicture is not null ? $"data:image/*;base64,{Convert.ToBase64String(user.ProfilePicture)}" : 
            "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Windows_10_Default_Profile_Picture.svg/2048px-Windows_10_Default_Profile_Picture.svg.png";
    }
    
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string ProfilePicture { get; set; } = null!;

    public IFormFile? NewProfilePicture { get; set; }

    public bool IsAdmin { get; set; }
}