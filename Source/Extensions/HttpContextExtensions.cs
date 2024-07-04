namespace TDP.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetCurrentUserId(this HttpContext context)
    {
        return new Guid(context.Session.GetString("user-id")!);
    }
}