using System.Reflection;
using Mapster;
using MapsterMapper;
using Moq;
using Questao5.Application.ContaCorrente.Commands;
using Questao5.Application.ContaCorrente.Queries;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;
using Questao5.Tests.Mocks;
using Shouldly;

namespace Questao5.Tests.CC.Queries;

public class InsertMovimentoCommandHandlerTest
{
    private readonly Mock<IContaCorrenteRepository> _mockRepo;
    private readonly IMapper _mapper;

    public InsertMovimentoCommandHandlerTest()
    {
        _mockRepo = MockContaCorrenteRepository.GetContaCorrenteTypeRepository();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        //config.Scan(typeof(IRegister).Assembly);
        _mapper = new Mapper(config);
    }

    [Fact]
    public async Task InsertMovimentoTest()
    {
        var handler = new InsertMovimentoCommandHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new InsertMovimentoCommand(NumeroContaCorrente: 123, TipoMovimento: 'C', Valor: 50.00M), CancellationToken.None);

        result.IsT0.ShouldBeTrue();

        result.AsT0.ShouldBeOfType<MovimentoResponse>();

        var hndlrSaldo = new GetSaldoQueryHandler(_mockRepo.Object, _mapper);

        var rsltSaldo = await hndlrSaldo.Handle(new GetSaldoQuery(NumeroContaCorrente: 123), CancellationToken.None);
        rsltSaldo.AsT0.Valor.ShouldBe(150.00M, 0.001M);

    }
}

