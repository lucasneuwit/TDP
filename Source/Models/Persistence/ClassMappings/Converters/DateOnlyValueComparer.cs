using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TDP.Models.Persistence;

public class DateOnlyValueComparer : ValueComparer<DateOnly>
{
    public DateOnlyValueComparer() : base(
        (d1, d2) => d1.DayNumber == d2.DayNumber,
        d => d.GetHashCode())
    {
    }
}
