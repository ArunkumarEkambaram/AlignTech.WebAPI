using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlignTech.WebAPI.DataFirst.Filters
{
    public class IpWishListResourceFilter : IResourceFilter
    {
        private readonly ILogger<IpWishListResourceFilter> _logger;
        private readonly string[] _allowedIps;

        public IpWishListResourceFilter(ILogger<IpWishListResourceFilter> logger, string[] allowedIps)
        {
            _logger = logger;
            _allowedIps = allowedIps;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var clientIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            if(clientIp == null || !_allowedIps.Contains(clientIp))
            {
                _logger.LogError($"Access Denied for your IP :{clientIp}");
                context.Result  = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }       
    }
}
