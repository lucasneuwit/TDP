namespace TDP.Models.Application.Services;

public class OmdbProvider : IApiProvider
{
    private readonly HttpClient client;
    private readonly string url;

    public OmdbProvider(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        this.url = configuration.GetValue<string>("ApiUrl");
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