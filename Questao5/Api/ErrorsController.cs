// using Microsoft.AspNetCore.Diagnostics;
// using Microsoft.AspNetCore.Mvc;

// namespace Questao5.Api;

// public class ErrorsController : ControllerBase
// {
//     [Route("/error")]
//     public IActionResult HandleError()
//     {
//         var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
//         var (statusCode, message) = exception switch
//         {
//             IServiceException serviceException => (
//                 (int)serviceException.StatusCode, serviceException.ErrorMessage),
//             _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
//         };

//         return Problem(statusCode: statusCode, title: message);

//     }
// }