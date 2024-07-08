using TDP.Models.Domain;

namespace TDP.Models.Persistence.Specifications
{
    public class MovieIncludeSpecification:IncludeSpecification<Movie>
    {
        public MovieIncludeSpecification()
        {
            this.Include(e => e.Followers);
        }
    }
}
