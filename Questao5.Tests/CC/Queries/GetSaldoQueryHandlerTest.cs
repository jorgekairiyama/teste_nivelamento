using System.Reflection;
using Mapster;
using MapsterMapper;
using Moq;
using Questao5.Application.ContaCorrente.Queries;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;
using Questao5.Tests.Mocks;
using Shouldly;

namespace Questao5.Tests.CC.Queries;

public class GetSaldoQueryHandlerTest
{
    private readonly Mock<IContaCorrenteRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetSaldoQueryHandlerTest()
    {
        _mockRepo = MockContaCorrenteRepository.GetContaCorrenteTypeRepository();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        //config.Scan(typeof(IRegister).Assembly);
        _mapper = new Mapper(config);
    }

    [Fact]
    public async Task GetSaldoContaCorrenteTest()
    {
        var handler = new GetSaldoQueryHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetSaldoQuery(NumeroContaCorrente: 123), CancellationToken.None);

        result.IsT0.ShouldBeTrue();

        result.AsT0.ShouldBeOfType<SaldoResponse>();

        result.AsT0.Valor.ShouldBe(100.00M, 0.001M);

    }
}

