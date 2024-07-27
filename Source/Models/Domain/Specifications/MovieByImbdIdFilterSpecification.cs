using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class MovieByImbdIdFilterSpecification : FilterSpecification<Movie>
{
    public MovieByImbdIdFilterSpecification(string imdbId)
    {
        this.expression = x => x.ImdbId == imdbId;
    }
}