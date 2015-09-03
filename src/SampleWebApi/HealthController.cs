using Microsoft.AspNet.Mvc;

namespace Library.WebApi
{
	[Route("api/[controller]")]
	public class HealthController : Controller
	{
		/// <summary>
		/// Performs a quick health check on the service.
		/// </summary>
		public IActionResult Get()
		{
			return new NoContentResult();
		}
	}
}
