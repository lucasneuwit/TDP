using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(entity => entity.Username).IsUnique();
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.Username);
        builder.Property(entity => entity.Name);
        builder.Property(entity => entity.LastName);
        builder.Property(entity => entity.BirthDay);
        builder.Property(entity => entity.EmailAddress);
        
        builder.HasMany(entity => entity.FollowedMovies);
    }
}