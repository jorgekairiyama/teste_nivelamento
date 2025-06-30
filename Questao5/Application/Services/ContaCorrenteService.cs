// using Questao5.Domain.Entities;
// using Questao5.Domain.Repositories.Interfaces;

// namespace Questao5.Application.Services;

// public class ContaCorrenteService : IContaCorrenteService
// {
//     private readonly IContaCorrenteRepository _contaCorrenteRepository;

//     public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
//     {
//         _contaCorrenteRepository = contaCorrenteRepository;
//     }

//     public async Task<ContaCorrenteDto> Get(int numeroContaCorrente)
//     {
//         var cc = await _contaCorrenteRepository.Get(numeroContaCorrente);

//         if (cc is not null)
//         {
//             return new ContaCorrenteDto
//             {
//                 Numero = cc.Numero,
//                 IdContaCorrente = cc.IdContaCorrente,
//                 Nome = cc.Nome
//             };
//         }
//         throw new Exception("Conta Corrente não encontrada.");
//     }

//     public async Task<SaldoDto> GetSaldo(int numeroContaCorrente)
//     {
//         var saldo = await _contaCorrenteRepository.GetSaldo(numeroContaCorrente);
//         if (saldo is not null)
//         {
//             return new SaldoDto
//             {
//                 IdContaCorrente = saldo.IdContaCorrente,
//                 Numero = saldo.Numero,
//                 Nome = saldo.Nome,
//                 Saldo = saldo.Valor
//             };
//         }
//         throw new Exception("Saldo não encontrado.");
//     }

//     public async Task<MovimentoDto> PostMovimento(MovimentoDto movimento)
//     {
//         var cc = await _contaCorrenteRepository.Get(movimento.NumeroContaCorrente);

//         if (cc is not null)
//         {
//             var mov = new Movimento
//             {
//                 IdMovimento = Guid.NewGuid().ToString(),
//                 DataMovimento = DateTime.UtcNow,
//                 IdContaCorrente = cc.IdContaCorrente,
//                 TipoMovimento = char.ToUpper(movimento.TipoMovimento),
//                 Valor = movimento.Valor
//             };
//             await _contaCorrenteRepository.PostMovimento(mov, movimento.NumeroContaCorrente);

//             return movimento;
//         }
//         throw new Exception("Conta Corrente não encontrada.");        
//     }
// }