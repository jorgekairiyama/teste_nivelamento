using OneOf;
using Questao5.Application.Common;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<OneOf<ContaCorrente, Error>> Get(int numeroContaCorrente);
        Task<OneOf<Saldo, Error>> GetSaldo(int numeroContaCorrente);
        Task<OneOf<Movimento, Error>> PostMovimento(Movimento movimento, int NumeroContaCorrente);

    }
}