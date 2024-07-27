using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using TDP.Models.Application;
using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;
using TDP.Models.Encryption;
using TDP.Models.Persistence;

namespace TDP.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers serilog and configures it with a file sink.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
    /// <returns>Itself, for chaining.</returns>
    public static IServiceCollection AddSerilog(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSerilog((services, config) =>
        {
            config.WriteTo.Console(
                LogEventLevel.Information,
                theme: AnsiConsoleTheme.Code);
            config.WriteTo.File(
                "log.txt",
                LogEventLevel.Debug,
                rollingInterval: RollingInterval.Day);
        });
    }
    
    /// <summary>
    /// Registers a <see cref="IEncryptor"/> implementation.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> to be used.</param>
    /// <returns>Itself, for chaining.</returns>
    public static IServiceCollection AddEncryption(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<EncryptionOptions>(configuration.GetSection(nameof(EncryptionOptions)));
        return serviceCollection.AddSingleton<IEncryptor, AesEncryptor>();
    }
    
    public static IServiceCollection AddOmdbHttpClient(this IServiceCollection serviceCollection, string apiUrl)
    {
        serviceCollection.AddHttpClient("OMDBApi",httpClient => 
        { 
            httpClient.BaseAddress = new Uri(apiUrl); 
        });
        return serviceCollection;
    }

    public static IServiceCollection AddMovieService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IMovieService, MovieService>();
    }

    public static IServiceCollection AddUserService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IUserService, UserService>();
    }

    public static IServiceCollection AddOmdbProvider(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IApiProvider, OmdbProvider>();
    }

    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, string connectionString)
    {
        return serviceCollection.AddDbContext<TdpDbContext>(opts => opts.UseSqlServer(connectionString));
    }

    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MovieMappingProfile());
        });
        
        return serviceCollection.AddSingleton(mappingConfig.CreateMapper());
    }
}