using TDP.Models.Domain;
using TDP.Models.Domain.Specification;

namespace TDP.Models.Persistence.Extensions
{
    public static class MovieRepositoryExtensionscs
    {
        public static Task<Movie?> FindByImdbId(this IRepository<Movie> repository,string imdbId)
        {
            var specification = new MovieByImbdIdFilterSpecification(imdbId);
            return repository.FirstOrDefaultAsync(specification);
        }

        public class MovieByImbdIdFilterSpecification : FilterSpecification<Movie>
        {
            public MovieByImbdIdFilterSpecification(string imdbId)
            {
                this.expression = x => x.ImdbId == imdbId;
            }
        }
    }
}
