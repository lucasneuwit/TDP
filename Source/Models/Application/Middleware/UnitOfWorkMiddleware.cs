using TDP.Models.Persistence.UnitOfWork;

namespace TDP.Models.Application.Middleware;

public class UnitOfWorkMiddleware
{
    private readonly RequestDelegate _next;

    public UnitOfWorkMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        if (context.Request.Method == HttpMethods.Post)
        {
            var _unitOfWorkManager = context.RequestServices.GetRequiredService<IUnitOfWorkManager>();
            _unitOfWorkManager.BeginUnitOfWork();

            await _next(context);

            await _unitOfWorkManager.Complete();
        }
        else
        {
            await _next(context);
        }
        
    }
}