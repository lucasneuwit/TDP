using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class UserRatingEntityTypeConfiguration : IEntityTypeConfiguration<UserRating>
{
    public void Configure(EntityTypeBuilder<UserRating> builder)
    {
        builder.HasKey(entity => new { entity.MovieId, entity.UserId});
        builder.Property(e => e.Rating);
        builder.Property(e => e.Comment);
        builder.HasOne<User>(e => e.User).WithMany(e => e.RatedMovies);
        builder.HasOne<Movie>(e => e.Movie).WithMany(e => e.Ratings);
    }
}