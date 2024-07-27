using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;
using TDP.Models.Encryption;

namespace TDP.Models.Persistence;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasDiscriminator<string>("user_type")
            .HasValue<User>("User")
            .HasValue<Administrator>("Administrator");
        
        builder.HasIndex(entity => entity.Username).IsUnique();
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.Username);
        builder.Property(entity => entity.Name);
        builder.Property(entity => entity.LastName);
        builder.Property(entity => entity.BirthDay);
        builder.Property(entity => entity.EmailAddress);
        builder.Property(entity => entity.ProfilePicture);
        builder.Property(entity => entity.PasswordHash);

        builder.HasMany(entity => entity.FollowedMovies).WithMany(movie => movie.Followers);
        builder.HasMany(entity => entity.RatedMovies);
    }
}