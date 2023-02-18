using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class UserRatingEntityTypeConfiguration : IEntityTypeConfiguration<UserRating>
{
    public void Configure(EntityTypeBuilder<UserRating> builder)
    {
        builder.HasKey(entity => new { entity.MovieId, entity.UserId});
        
        builder.Property(entity => entity.MovieId);
        builder.Property(entity => entity.UserId);
        builder.Property(entity => entity.Rating);
    }
}