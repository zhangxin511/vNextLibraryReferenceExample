using Microsoft.AspNet.Mvc;

namespace Library.WebApi
{
    [AllowAnonymous]
    public class VersionController : Controller
    {
		[HttpGet, Produces(typeof(string))]
		public IActionResult Get()
        {
            return new ObjectResult( Service.Version );
        }
    }
}
