using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services
{
   public interface IContaCorrenteDataService
    {
        Task<Questao5.Domain.Entities.ContaCorrente?> Get(int numeroContaCorrente);
        Task<Saldo> GetSaldo(int numeroContaCorrente);
        Task<Movimento> PostMovimento(Movimento movimento);
    } 
}