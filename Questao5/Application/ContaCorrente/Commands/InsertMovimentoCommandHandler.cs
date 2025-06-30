using MapsterMapper;
using MediatR;
using OneOf;
using Questao5.Application.Common;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;

namespace Questao5.Application.ContaCorrente.Commands;

public class InsertMovimentoCommandHandler : IRequestHandler<InsertMovimentoCommand, OneOf<MovimentoResponse, Error>>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMapper _mapper;

    public InsertMovimentoCommandHandler(
        IContaCorrenteRepository contaCorrenteRepository,
        IMapper mapper)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<MovimentoResponse, Error>> Handle(InsertMovimentoCommand request, CancellationToken cancellationToken)
    {
        var mov = _mapper.Map<Movimento>(request);
        var result = await _contaCorrenteRepository.PostMovimento(mov, request.NumeroContaCorrente);

        if (result.IsT0)
            return _mapper.Map<MovimentoResponse>(result.AsT0);
        else
            return result.AsT1;
    }
}

