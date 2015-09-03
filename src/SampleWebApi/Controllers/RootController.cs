using Microsoft.AspNet.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RootController : Controller
    {
        public IActionResult Get()
        {
			//return Redirect( Request.RequestUri.AbsoluteUri + "swagger/ui/index" );
			return Redirect(Request.PathBase.ToUriComponent() + "swagger/ui/index");
		}
    }
}
