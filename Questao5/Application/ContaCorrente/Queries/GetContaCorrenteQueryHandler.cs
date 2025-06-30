using MapsterMapper;
using MediatR;
using NSubstitute.Routing.Handlers;
using OneOf;
using Questao5.Application.Common;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Application.ContaCorrente.Queries;

public class GetContaCorrenteQueryHandler : IRequestHandler<GetContaCorrenteQuery, OneOf<ContaCorrenteResponse, Error>>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMapper _mapper;

    public GetContaCorrenteQueryHandler(
        IContaCorrenteRepository contaCorrenteRepository,
        IMapper mapper)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<ContaCorrenteResponse, Error>> Handle(GetContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        var result = await _contaCorrenteRepository.Get(request.NumeroContaCorrente);

        if (result.IsT0)
            return _mapper.Map<ContaCorrenteResponse>(result.AsT0);
        else
            return result.AsT1;

    }
}

