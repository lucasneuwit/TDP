namespace TDP.Extensions;

public static class Extension
{
    public static Guid ToGuid(this Guid? source)
    {
        return source ?? Guid.Empty;
    }
}