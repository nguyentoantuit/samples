using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.ASP.Web.Configuration;
using System;

namespace Sample.ASP.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/config")]
    public class ConfigurationController : Controller
    {
        private readonly AppSettingOptions _appSettingOptions;
        private readonly ConnectionStrings _connectionStrings;

        public ConfigurationController(IOptions<AppSettingOptions> configureOptions, IOptions<ConnectionStrings> connectionOptions)
        {
            _appSettingOptions = configureOptions.Value ?? throw new ArgumentNullException(nameof(configureOptions));
            _connectionStrings = connectionOptions.Value ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_appSettingOptions);
        }

        [HttpGet("connections")]
        public IActionResult GetConnectionStrings()
        {
            return Ok(_connectionStrings);
        }
    }
}
