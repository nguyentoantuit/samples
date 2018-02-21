using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ASP.Web.Services;
using System;

namespace Sample.ASP.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/log")]
    public class LoggingController : Controller
    {
        private readonly ILogger<LoggingController> _logger;
        private readonly IResourceService _resourceService;
        public LoggingController(ILogger<LoggingController> logger, IResourceService resourceService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogTrace("LoggingController - Log Trace");
            _logger.LogDebug("LoggingController - Log Debug");
            _logger.LogInformation("LoggingController - Log Information");
            _logger.LogWarning("LoggingController - Log Warning");
            _logger.LogError("LoggingController - Log Error");
            _logger.LogCritical("LoggingController - Log Critical");

            return _resourceService.GetResource();
        }
    }
}
