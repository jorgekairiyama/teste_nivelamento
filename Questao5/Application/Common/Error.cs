using Questao5.Application.Common.Enum;

namespace Questao5.Application.Common;

public record Error(ErrorType Code, string Message);
