namespace TDP.Models.Application.Services;

using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using System.Web.Http;
using TDP.Models.Application.DataTransfer;

public class OmdbProvider : IApiProvider
{
    private readonly IHttpClientFactory _clientFactory;

    public OmdbProvider(IHttpClientFactory clientFactory)
    {
        this._clientFactory = clientFactory;
    }
    
    public async Task<MovieDTO> FindAsync(IRequest request)
    {
        var aMovie = new MovieDTO();
        var httpClient = _clientFactory.CreateClient("OMDBApi");
        var queryParams = new Dictionary<string, string>()
        {
            {"t",request.Title },
            {"i",request.ImdbId },
            {"y",request.ReleaseYear },
            {"type",request.Type }
        };
        string url = QueryHelpers.AddQueryString(httpClient.BaseAddress.ToString(),queryParams);
        var apirequest = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await httpClient.SendAsync(apirequest);
        response.EnsureSuccessStatusCode();
        if (request.Type == "movie") { aMovie = JsonSerializer.Deserialize<MovieDTO>(await response.Content.ReadAsStringAsync()); }
        else { aMovie = JsonSerializer.Deserialize<SeriesDTO>(await response.Content.ReadAsStringAsync()); }
        
        return aMovie;
    }

    public async Task<MovieCollection> SearchAsync(IRequest request, int pageNumber)
    {
        var httpClient = _clientFactory.CreateClient("OMDBApi");
        var queryParams = new Dictionary<string, string>()
        {
            {"s",request.Title },
            {"y",request.ReleaseYear },
            {"type",request.Type },
            {"page",pageNumber.ToString()}
        };
        string url = QueryHelpers.AddQueryString(httpClient.BaseAddress.ToString(), queryParams);
        var apirequest = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await httpClient.SendAsync(apirequest);
        response.EnsureSuccessStatusCode();
        MovieCollection aMovieCollection = JsonSerializer.Deserialize<MovieCollection>(await response.Content.ReadAsStringAsync());
        aMovieCollection.SearchString = request.Title;
        aMovieCollection.CurrentPage = pageNumber;
        aMovieCollection.Type = request.Type;
        aMovieCollection.Released = request.ReleaseYear;
        return aMovieCollection;
    }
}