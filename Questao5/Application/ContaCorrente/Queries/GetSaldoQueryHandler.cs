using MapsterMapper;
using MediatR;
using OneOf;
using Questao5.Application.Common;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Application.ContaCorrente.Queries;

public class GetSaldoQueryHandler : IRequestHandler<GetSaldoQuery, OneOf<SaldoResponse, Error>>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMapper _mapper;

    public GetSaldoQueryHandler(
        IContaCorrenteRepository contaCorrenteRepository,
        IMapper mapper)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<SaldoResponse, Error>> Handle(GetSaldoQuery request, CancellationToken cancellationToken)
    {
        var result = await _contaCorrenteRepository.GetSaldo(request.NumeroContaCorrente);

        if (result.IsT0)
            return _mapper.Map<SaldoResponse>(result.AsT0);
        else
            return result.AsT1;
    }
}

