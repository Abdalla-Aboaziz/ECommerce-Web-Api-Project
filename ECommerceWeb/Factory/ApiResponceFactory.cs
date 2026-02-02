using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Factory
{
    public static class ApiResponceFactory
    {
        public static IActionResult GenerateApiValidationResponce(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                   .Where(e => e.Value.Errors.Count > 0)
                   .ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage).ToArray());

            var problem = new ProblemDetails
            {
                Title = "Validation Errors",
                Status = StatusCodes.Status400BadRequest,
                Detail = "one or more validation error ",
                Extensions = { ["Error"] = errors }
            };
            return new BadRequestObjectResult(problem);
        }
    }
}
