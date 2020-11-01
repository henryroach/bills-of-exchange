using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace BillsOfExchange
{
    public class LogInterceptor : IInterceptor
    {
        private readonly ILogger<LogInterceptor> _logger;

        public LogInterceptor(ILogger<LogInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogInformation($"{invocation.Method.DeclaringType}.{invocation.Method.Name} invoked");

            invocation.Proceed();
        }
    }
}