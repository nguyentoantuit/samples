using Microsoft.Extensions.Logging;
using System;

namespace Sample.ASP.Web.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ILogger<ResourceService> _logger;

        public ResourceService(ILogger<ResourceService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetResource()
        {
            _logger.LogTrace("ResourceService - GetResource() - Log Trace");
            _logger.LogDebug("ResourceService - GetResource() - Log Debug");
            _logger.LogInformation("ResourceService - GetResource() - Log Information");
            _logger.LogWarning("ResourceService - GetResource() - Log Warning");
            _logger.LogError("ResourceService - GetResource() - Log Error");
            _logger.LogCritical("ResourceService - GetResource() - Log Critical");

            return "Return value";
        }
    }
}
