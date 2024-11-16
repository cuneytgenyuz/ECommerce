using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Helpers;

public class ResponseHelper
{
    public static IActionResult Ok(string message, object data = null)
    {
        return new JsonResult(new { Code = "200", Message = message, Success = true, Data = data })
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public static IActionResult CreatedAtAction(string actionName, object routeValues, string message, object data)
    {
        return new JsonResult(new { Code = "201", Message = message, Success = true, Data = data })
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    public static IActionResult BadRequest(string message)
    {
        return new JsonResult(new { Code = "400", Message = message, Success = false })
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult NotFound(string message)
    {
        return new JsonResult(new { Code = "404", Message = message, Success = false })
        {
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public static IActionResult NoContent()
    {
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }
}