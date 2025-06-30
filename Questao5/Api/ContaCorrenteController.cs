using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.Routing.Handlers;
using Questao5.Api;
using Questao5.Application.ContaCorrente.Commands;
using Questao5.Application.ContaCorrente.Queries;
using Questao5.Domain.Repositories.Interfaces;
using Questao5.Infrastructure.ContaCorrente;


namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("[controller]")]
    public class ContaCorrenteController : ApiController
    {

        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteController(
            ILogger<ContaCorrenteController> logger,
            IMapper mapper,
            ISender mediator,
            IContaCorrenteRepository contaCorrenteRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _contaCorrenteRepository = contaCorrenteRepository;
        }


        [HttpGet]
        [Route("{numeroContaCorrente}")]
        public async Task<ActionResult<ContaCorrenteResponse>> Get(int numeroContaCorrente)
        {
            try
            {
                var query = new GetContaCorrenteQuery(numeroContaCorrente);
                var result = await _mediator.Send(query);

                if (result.IsT0)
                    return Ok(result.AsT0);
                else
                    return Problem(statusCode: (int)result.AsT1.Code, title: result.AsT1.Message);
            }
            catch (Exception ex)
            {
                const string errmsg = "Erro ao obter conta corrente.";
                _logger.LogError(ex, errmsg);
                return Problem(statusCode: (int)HttpStatusCode.InternalServerError, title: errmsg);
            }
        }

        [HttpGet]
        [Route("saldo/{numeroContaCorrente}")]
        public async Task<ActionResult<SaldoResponse>> GetSaldo(int numeroContaCorrente)
        {
            try
            {
                var query = new GetSaldoQuery(numeroContaCorrente);

                var result = await _mediator.Send(query);
                if (result.IsT0)
                    return Ok(result.AsT0);
                else
                    return Problem(statusCode: (int)result.AsT1.Code, title: result.AsT1.Message);
            }
            catch (Exception ex)
            {
                const string errmsg = "Erro ao obter saldo da conta corrente.";
                _logger.LogError(ex, errmsg);
                return Problem(statusCode: (int)HttpStatusCode.InternalServerError, title: errmsg);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovimentoResponse>> PostMovimento([FromBody] MovimentoRequest movimento)
        {
            try
            {
                var command = _mapper.Map<InsertMovimentoCommand>(movimento);
                var result = await _mediator.Send(command);
                if (result.IsT0)
                    return Ok(result.AsT0);
                else
                    return Problem(statusCode: (int)result.AsT1.Code, title: result.AsT1.Message);
            }
            catch (Exception ex)
            {
                const string errmsg = "Erro ao registrar movimento na conta corrente.";
                _logger.LogError(ex, errmsg);
                return Problem(statusCode: (int)HttpStatusCode.InternalServerError, title: errmsg);
            }
        }
    }
}