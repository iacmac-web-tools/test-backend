using MediatR;
using Microsoft.Extensions.Logging;

namespace Theses.Application.Common.Behaviours;

public class ExceptionHandlingBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionHandlingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}",
                requestName, request);

            throw;
        }
    }
}
