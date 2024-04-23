
using TDP.Models.Application.DataTransfer;

namespace TDP.Models.Application;

/// <summary>
/// Defines a contract for external API providers.
/// </summary>
public interface IApiProvider
{
    Task<MovieDTO> FindAsync(IRequest request);

    Task<MovieCollection> SearchAsync(IRequest request, int pageNumber);
}