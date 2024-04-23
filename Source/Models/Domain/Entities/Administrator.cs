namespace TDP.Models.Domain;

public class Administrator : User
{
    public Administrator(Guid id) : base(id)
    {
        this.IsAdministrator = true;
    }
}