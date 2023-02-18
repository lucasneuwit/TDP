using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TDP.Models.Persistence;

internal class DateOnlyValueConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyValueConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}
