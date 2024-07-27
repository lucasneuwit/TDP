using TDP.Models.Domain;

namespace TDP.Models.Persistence
{
    /// <summary>
    /// Defines extensions over <see cref="IRepository{TEntity}"/>.
    /// </summary>
    public static class MovieRepositoryExtensions
    {
        /// <summary>
        /// Finds a <see cref="Movie"/> by its imdb id.
        /// </summary>
        /// <param name="repository">The <see cref="IRepository{TEntity}"/> instance.</param>
        /// <param name="imdbId">The imdb id.</param>
        /// <param name="includeSpecification">A <see cref="IIncludeSpecification{T}"/> for data aggregation.</param>
        /// <returns>The <see cref="Movie"/> instance, if found.</returns>
        public static Task<Movie?> FindByImdbId(this IRepository<Movie> repository,string imdbId, IIncludeSpecification<Movie> includeSpecification)
        {
            var aggregateSpec = new AggregateSpecification<Movie>();
            aggregateSpec.AddFilter(new MovieByImbdIdFilterSpecification(imdbId));
            aggregateSpec.AddInclude(includeSpecification);
            return repository.FirstOrDefaultAsync(aggregateSpec);
        }
    }
}
