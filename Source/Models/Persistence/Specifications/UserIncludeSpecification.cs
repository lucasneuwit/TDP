using TDP.Models.Domain;

namespace TDP.Models.Persistence
{
    public class UserIncludeSpecification: IncludeSpecification<User>
    {
        public UserIncludeSpecification()
        {
            this.Include(e => e.FollowedMovies);
            this.Include(e => e.RatedMovies);
        }
    }
}
