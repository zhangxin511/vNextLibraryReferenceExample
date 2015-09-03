using Microsoft.AspNet.Mvc;

namespace HttpEx
{
    public static class ApiControllerExtensions
    {
        public static string GetRequestUriString( this Controller controller )
        {
			return controller.Request.Path.ToString();// controller.Request.RequestUri.ToString();
        }

        public static string LinkTo( this Controller controller, string routeName, object routeValues )
        {
            return controller.Url.Link( routeName, routeValues );
        }
    }
}
