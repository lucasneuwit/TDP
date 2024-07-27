using TDP.Models.Persistence;

namespace TDP.Models.Application;

public class UnitOfWorkMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {

        if (context.Request.Method == HttpMethods.Post)
        {
            var unitOfWorkManager = context.RequestServices.GetRequiredService<IUnitOfWorkManager>();
            unitOfWorkManager.BeginUnitOfWork();

            await next(context);

            await unitOfWorkManager.Complete();
        }
        else
        {
            await next(context);
        }
        
    }
}