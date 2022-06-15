using Inventory.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Behaviours
{
    /// <summary>
    /// Class for handling unhandled exceptions. 
    /// It is centralized in this class, so we don't have to include try/catch blocks in each of the handlers.
    /// We perform this surrounding the current handler operation with try/catch block
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch(ValidationException valEx)
            {
                //return StatusCode(422, valEx.Errors);
                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
