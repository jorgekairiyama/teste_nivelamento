using System.Net;

namespace Questao5.Application.Common.Enum;

public enum ErrorType
{
    NoError,
    Validation = HttpStatusCode.BadRequest,
    Conflict = HttpStatusCode.Conflict,
    NotFound = HttpStatusCode.NotFound,
    Failure = HttpStatusCode.NotImplemented,
    Forbidden = HttpStatusCode.Forbidden,
    Unauthorized = HttpStatusCode.Unauthorized
}