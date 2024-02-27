using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class SeriesEntityTypeConfiguration : IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.Property(entity => entity.Seasons);
        //builder.HasMany(entity => entity.Episodes).WithOne(entity => entity.Series).OnDelete(DeleteBehavior.NoAction);
    }
}