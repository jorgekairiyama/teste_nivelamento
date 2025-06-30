using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services
{
    public class ContaCorrenteDataService : IContaCorrenteDataService
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteDataService(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }


        public async Task<Domain.Entities.ContaCorrente?> Get(int numeroContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var cc = await connection.QuerySingleOrDefaultAsync<Domain.Entities.ContaCorrente>("select idcontacorrente, numero, nome, ativo from contacorrente where numero = @numeroContaCorrente",
                new { numeroContaCorrente });
            return cc;
        }

        public async Task<Saldo> GetSaldo(int numeroContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            string query = @"select 
                            cc.idcontacorrente, 
                            cc.numero, 
                            cc.nome, 
                            sum(case 
                                    when m.tipomovimento = 'D' then - m.valor
                                    else m.valor
                                end) as valor " +
                           "from contacorrente cc " +
                           "left join movimento m on cc.idcontacorrente = m.idcontacorrente " +
                           "where cc.numero = @numeroContaCorrente " +
                           "group by cc.idcontacorrente, cc.numero, cc.nome";

            var saldo = await connection.QuerySingleOrDefaultAsync<Saldo>(query, new { numeroContaCorrente });
            return saldo;
        }

        public async Task<Movimento> PostMovimento(Movimento movimento)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";
            var paramsMovimento = new
            {
                idmovimento = movimento.IdMovimento,
                idcontacorrente = movimento.IdContaCorrente,
                datamovimento = movimento.DataMovimento,
                tipomovimento = (char)movimento.TipoMovimento,
                valor = movimento.Valor
            };
            var rowsAffected = await connection.ExecuteAsync(sql, paramsMovimento);

            return movimento;
        }
    }

}