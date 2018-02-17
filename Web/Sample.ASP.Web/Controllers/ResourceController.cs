using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace Sample.ASP.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/resource")]
    public class ResourceController : Controller
    {
        private IStringLocalizer<ResourceController> _stringLocalizer;
        private IStringLocalizer<SharedResource> _sharedLocalizer;
        public ResourceController(IStringLocalizer<ResourceController> stringLocalizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _stringLocalizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        [HttpGet]
        public string Get()
        {
            return _stringLocalizer["TestResource"].Value;
        }

        [HttpGet("sharedResource")]
        public string GetSharedResource()
        {
            return _sharedLocalizer["TestResource"].Value;
        }
    }
}
