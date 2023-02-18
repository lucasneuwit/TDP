namespace TDP.Models.Application.Services;

public class OmdbProvider : IApiProvider
{
    private readonly HttpClient client;

    public OmdbProvider(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Movie> FindAsync(IRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> SearchAsync(IRequest request, int pageNumber)
    {
        throw new NotImplementedException();
    }
}