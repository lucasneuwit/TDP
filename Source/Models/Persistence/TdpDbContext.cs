using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class TdpDbContext : DbContext
{
    public TdpDbContext(DbContextOptions options)
        : base(options)
    {
        this.Database.Migrate();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateOnly>().HaveConversion<DateOnlyValueConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TdpDbContext).Assembly);
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var dataToSeed = new DataSeed();
        builder.Entity<Movie>().HasData(dataToSeed.MoviesToSeed);
    }
}