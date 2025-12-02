using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Enums
{
    public static class ResponseSender
    {
        public static ActionResult<T> Send200<T>(this ControllerBase controller, T response)
        {
            return controller.Ok(response);
        }

        public static ActionResult Send400(this ControllerBase controller, string response)
        {
            return controller.BadRequest(response);
        }

        public static ActionResult Send404(this ControllerBase controller, string response)
        {
            return controller.NotFound(response);
        }

        public static ActionResult Send500(this ControllerBase controller, string response)
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
