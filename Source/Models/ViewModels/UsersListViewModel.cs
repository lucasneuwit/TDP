namespace TDP.Models.ViewModels;

public class UsersListViewModel
{
    public UsersListViewModel(Guid currentUserId, ICollection<UserViewModel> users)
    {
        this.CurrentUser = users.Single(x => x.Id == currentUserId);
        this.Users = users;
    }
    
    public UserViewModel CurrentUser { get; set; }
    
    public ICollection<UserViewModel> Users { get; set; }
}