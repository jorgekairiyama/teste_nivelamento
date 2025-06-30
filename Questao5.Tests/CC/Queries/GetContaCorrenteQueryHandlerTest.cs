using System.Diagnostics.Contracts;
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

public class GetContaCorrenteQueryHandlerTest
{
    private readonly Mock<IContaCorrenteRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetContaCorrenteQueryHandlerTest()
    {
        _mockRepo = MockContaCorrenteRepository.GetContaCorrenteTypeRepository();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        //config.Scan(typeof(IRegister).Assembly);
        _mapper = new Mapper(config);
    }

    [Fact]
    public async Task GetContaCorrenteTest()
    {
        var handler = new GetContaCorrenteQueryHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetContaCorrenteQuery(NumeroContaCorrente: 123), CancellationToken.None);

        result.IsT0.ShouldBeTrue();

        result.IsT0.ShouldBeOfType<ContaCorrenteResponse>();

    }
}

