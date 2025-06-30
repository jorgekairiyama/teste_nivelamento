using Moq;
using OneOf;
using Questao5.Application.Common;
using Questao5.Application.Common.Enum;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Interfaces;

namespace Questao5.Tests.Mocks;

public static class MockContaCorrenteRepository
{

    public static Mock<IContaCorrenteRepository> GetContaCorrenteTypeRepository()
    {
        var mockRepo = new Mock<IContaCorrenteRepository>();

        var contaCorrente = new List<ContaCorrente>
        {
            new() {
                IdContaCorrente ="B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Numero = 123,
                Nome = "Katherine Sanchez",
                Ativo = true
            },
            new() {
                IdContaCorrente ="382D323D-7067-ED11-8866-7D5DFA4A16C9",
                Numero = 789,
                Nome = "Tevin Mcconnell",
                Ativo = true
            },
            new() {
                IdContaCorrente ="F475F943-7067-ED11-A06B-7E5DFA4A16C9",
                Numero = 741,
                Nome = "Ameena Lynn",
                Ativo = false
            }
        };

        var movimento = new List<Movimento>
        {
            new() {
                IdMovimento = "2301a5d8-c26c-40d3-bf7b-6d3315e048b0",
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                DataMovimento = DateTime.Today.ToUniversalTime(),
                TipoMovimento = 'C',
                Valor = 100.00M
            }
        };



        mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int nCC) =>
        {
            var conta = contaCorrente.FirstOrDefault(cc => cc.Numero == nCC);
            if (conta != null)
            {
                if (!conta.Ativo)
                {
                    return OneOf<ContaCorrente, Error>.FromT1(new Error(Code: ErrorType.Conflict, Message: "INACTIVE_ACCOUNT"));
                }

                return OneOf<ContaCorrente, Error>.FromT0(conta);
            }
            else
            {
                return OneOf<ContaCorrente, Error>.FromT1(new Error(Code: ErrorType.NotFound, Message: "INVALID_ACCOUNT"));
            }
        });

        mockRepo.Setup(r => r.GetSaldo(It.IsAny<int>())).ReturnsAsync((int nCC) =>
        {
            var cc = contaCorrente.FirstOrDefault(cc => cc.Numero == nCC);


            if (cc is not null)
            {
                if (!cc.Ativo)
                {
                    return OneOf<Saldo, Error>.FromT1(new Error(Code: ErrorType.Conflict, Message: "INACTIVE_ACCOUNT"));
                }

                var saldo = movimento.Where(m => m.IdContaCorrente == cc.IdContaCorrente).Sum(m => m.TipoMovimento == 'C' ? m.Valor : -m.Valor);
                return OneOf<Saldo, Error>.FromT0(new Saldo { IdContaCorrente = cc.IdContaCorrente, Numero = cc.Numero, Nome = cc.Nome, Valor = saldo });

            }
            return OneOf<Saldo, Error>.FromT1(new Error(Code: ErrorType.NotFound, Message: "INVALID_ACCOUNT"));

        });

        mockRepo.Setup(r => r.PostMovimento(It.IsAny<Movimento>(), It.IsAny<int>())).ReturnsAsync((Movimento mov, int nCC) =>
        {
            var cc = contaCorrente.FirstOrDefault(cc => cc.Numero == nCC);
            if (cc is null)
            {
                return new Error(Code: ErrorType.NotFound, Message: "INVALID_ACCOUNT");
            }
            if (!cc.Ativo)
            {
                return new Error(Code: ErrorType.Conflict, Message: "INACTIVE_ACCOUNT");
            }
            if (mov.Valor <= 0)
            {
                return new Error(Code: ErrorType.Validation, Message: "INVALID_VALUE");
            }
            if (mov.TipoMovimento != 'C' && mov.TipoMovimento != 'D')
            {
                return new Error(Code: ErrorType.Validation, Message: "INVALID_TYPE");
            }

            mov.IdMovimento = Guid.NewGuid().ToString();
            mov.IdContaCorrente = cc.IdContaCorrente;
            mov.DataMovimento = DateTime.Now;
            movimento.Add(mov);

            return mov;
        });
        return mockRepo;
    }
}
