using Questao5.Domain.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services;
using Questao5.Application.Services;
using OneOf;
using Questao5.Application.Common;
using Questao5.Application.Common.Enum;

namespace questao5.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IContaCorrenteDataService _contaCorrenteDataService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ContaCorrenteRepository(IContaCorrenteDataService contaCorrenteDataService, IDateTimeProvider dateTimeProvider)
        {
            _contaCorrenteDataService = contaCorrenteDataService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<OneOf<ContaCorrente, Error>> Get(int numeroContaCorrente)
        {
            var (esValido, contaCorrente, msgError, errCode) = await EValidoContaCorrente(numeroContaCorrente);
            if (!esValido)
            {
                return new Error(Code: errCode, Message: msgError);
            }
            return contaCorrente!;
        }

        public async Task<OneOf<Saldo, Error>> GetSaldo(int numeroContaCorrente)
        {
            var (esValido, _, msgError, errCode) = await EValidoContaCorrente(numeroContaCorrente);
            if (!esValido)
            {
                return new Error(Code: errCode, Message: msgError);
            }
            return await _contaCorrenteDataService.GetSaldo(numeroContaCorrente);
        }

        public async Task<OneOf<Movimento, Error>> PostMovimento(Movimento movimento, int NumeroContaCorrente)
        {
            var (esValido, contaCorrente, msgError, errCode) = await EValidoContaCorrente(NumeroContaCorrente);
            if (!esValido)
            {
                return new Error(Code: errCode, Message: msgError);
            }
            if (movimento.Valor <= 0)
            {
                return new Error(Code: ErrorType.Validation, Message: "INVALID_VALUE");
            }
            if (movimento.TipoMovimento != 'C' && movimento.TipoMovimento != 'D')
            {
                return new Error(Code: ErrorType.Validation, Message: "INVALID_TYPE");
            }
            movimento.IdMovimento = Guid.NewGuid().ToString();
            movimento.IdContaCorrente = contaCorrente!.IdContaCorrente;
            movimento.DataMovimento = _dateTimeProvider.UtcNow;
            return await _contaCorrenteDataService.PostMovimento(movimento);
        }

        private async Task<(bool esValido, ContaCorrente? contaCorrente, string msgError, ErrorType code)> EValidoContaCorrente(int numeroContaCorrente)
        {
            var contaCorrente = await _contaCorrenteDataService.Get(numeroContaCorrente);
            if (contaCorrente is null)
            {
                return (false, null, "INVALID_ACCOUNT", ErrorType.NotFound);
            }

            if (!contaCorrente.Ativo)
            {
                return (false, contaCorrente, "INACTIVE_ACCOUNT", ErrorType.Conflict);
            }

            return (true, contaCorrente, string.Empty, ErrorType.NoError);
        }
    }
}