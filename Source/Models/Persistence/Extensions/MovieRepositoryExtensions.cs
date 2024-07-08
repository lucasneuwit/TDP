using TDP.Models.Domain;
using TDP.Models.Domain.Abstractions;
using TDP.Models.Domain.Specification;
using TDP.Models.Domain.Specifications;

namespace TDP.Models.Persistence.Extensions
{
    public static class MovieRepositoryExtensions
    {
        public static Task<Movie?> FindByImdbId(this IRepository<Movie> repository,string imdbId, IIncludeSpecification<Movie> includeSpecification)
        {
            var aggregateSpec = new AggregateSpecification<Movie>();
            aggregateSpec.AddFilter(new MovieByImbdIdFilterSpecification(imdbId));
            aggregateSpec.AddInclude(includeSpecification);
            return repository.FirstOrDefaultAsync(aggregateSpec);
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
