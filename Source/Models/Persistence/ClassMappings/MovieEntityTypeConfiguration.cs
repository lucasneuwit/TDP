using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.Title);
        builder.Property(entity => entity.Plot);
        builder.Property(entity => entity.Runtime);
        builder.Property(entity => entity.Type);
        builder.Property(entity => entity.Released);
        builder.Property(entity => entity.PosterUrl);
        builder.Property(entity => entity.Country);
        builder.Property(entity => entity.ImdbRating).HasPrecision(4,2);

        builder.OwnsMany(entity => entity.Participants, child =>
        {
            child.HasKey(entity => new {entity.MovieId, entity.Name, entity.Role});

            child.Property(entity => entity.MovieId);
            child.Property(entity => entity.Name);
            child.Property(entity => entity.Role);
        });
    }
}