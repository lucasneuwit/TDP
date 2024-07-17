using TDP.Models.Domain;

namespace TDP.Models.Persistence
{
    public class MovieIncludeSpecification:IncludeSpecification<Movie>
    {
        public MovieIncludeSpecification()
        {
            this.Include(e => e.Followers);
            this.Include(e => e.Ratings);
        }
    }
}
