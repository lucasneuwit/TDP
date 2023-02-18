
namespace TDP.Models.Application;

/// <summary>
/// Defines a contract for external API providers.
/// </summary>
public interface IApiProvider
{
    Task<Movie> FindAsync(IRequest request);

    Task<IEnumerable<Movie>> SearchAsync(IRequest request, int pageNumber);
}