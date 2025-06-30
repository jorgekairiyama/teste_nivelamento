using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Common;
using Questao5.Application.Common.Enum;

namespace Questao5.Api;

[ApiController]
public class ApiController : ControllerBase
{
    private IActionResult Problem(Error error)
    {
        var statusCode = error.Code switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status418ImATeapot,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Message);
    }

}