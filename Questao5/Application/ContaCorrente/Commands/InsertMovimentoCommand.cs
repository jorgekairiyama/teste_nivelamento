using MediatR;
using OneOf;
using Questao5.Application.Common;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Application.ContaCorrente.Commands
{
        public record InsertMovimentoCommand(
                int NumeroContaCorrente,
                char TipoMovimento,
                decimal Valor
        ) : IRequest<OneOf<MovimentoResponse, Error>>;

}