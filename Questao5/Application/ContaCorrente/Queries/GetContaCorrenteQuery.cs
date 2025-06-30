using MediatR;
using OneOf;
using Questao5.Application.Common;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Application.ContaCorrente.Queries;

public record GetContaCorrenteQuery(
    int NumeroContaCorrente
) : IRequest<OneOf<ContaCorrenteResponse, Error>>;

