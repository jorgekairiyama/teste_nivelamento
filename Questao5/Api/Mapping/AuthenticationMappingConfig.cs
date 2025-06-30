using Mapster;
using Questao5.Application.ContaCorrente.Commands;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContaCorrenteResponse, ContaCorrente>();
        config.NewConfig<SaldoResponse, Saldo>();
        config.NewConfig<Movimento, InsertMovimentoCommand>();
        config.NewConfig<MovimentoResponse, Movimento>();
    }
}