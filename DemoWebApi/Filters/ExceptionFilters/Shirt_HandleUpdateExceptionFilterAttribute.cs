using DemoWebApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebApi.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out var shirtId))
            {
                if (!ShirtRepository.ShirtExists(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt doesn't exists anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}
