using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class EpisodeEntityTypeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.Property(entity => entity.Number);
        builder.Property(entity => entity.Season);
    }
}