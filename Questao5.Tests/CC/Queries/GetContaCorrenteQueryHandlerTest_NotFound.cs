using System.Reflection;
using Mapster;
using MapsterMapper;
using Moq;
using Questao5.Application.Common;
using Questao5.Application.ContaCorrente.Queries;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;
using Questao5.Tests.Mocks;
using Shouldly;

namespace Questao5.Tests.CC.Queries;

public class GetContaCorrenteQueryHandlerTest_NotFound
{
    private readonly Mock<IContaCorrenteRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetContaCorrenteQueryHandlerTest_NotFound()
    {
        _mockRepo = MockContaCorrenteRepository.GetContaCorrenteTypeRepository();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        //config.Scan(typeof(IRegister).Assembly);
        _mapper = new Mapper(config);
    }

    [Fact]
    public async Task GetContaCorrenteNotExistsTest()
    {
        int NumeroContaCorrenteNaoExiste = 100;
        var handler = new GetContaCorrenteQueryHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetContaCorrenteQuery(NumeroContaCorrente: NumeroContaCorrenteNaoExiste), CancellationToken.None);

        result.IsT1.ShouldBeTrue();

        result.AsT1.ShouldBeOfType<Error>();
    }

    [Fact]
    public async Task GetContaCorrenteInactiveTest()
    {
        int NumeroContaCorrenteNaoExiste = 741;
        var handler = new GetContaCorrenteQueryHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetContaCorrenteQuery(NumeroContaCorrente: NumeroContaCorrenteNaoExiste), CancellationToken.None);

        result.IsT1.ShouldBeTrue();

        result.AsT1.ShouldBeOfType<Error>();

        result.AsT1.Message.ShouldBe("INACTIVE_ACCOUNT");
    }
}

