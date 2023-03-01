﻿namespace TDP.Models.Application.Services;

using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using System.Web.Http;

public class OmdbProvider : IApiProvider
{
    private readonly IHttpClientFactory _clientFactory;

    public OmdbProvider(IHttpClientFactory clientFactory)
    {
        this._clientFactory = clientFactory;
    }
    
    public async Task<Movie> FindAsync(IRequest request)
    {
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
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Movie aMovie = JsonSerializer.Deserialize<Movie>(await response.Content.ReadAsStringAsync());
        return aMovie;
    }

    public async Task<IEnumerable<Movie>> SearchAsync(IRequest request, int pageNumber)
    {
        throw new NotImplementedException();
    }
}

//byte[] jsonAsBytes = Encoding.UTF8.GetBytes(string.Join("&", postParams.Select(pp => pp.Key + "=" + pp.Value)));