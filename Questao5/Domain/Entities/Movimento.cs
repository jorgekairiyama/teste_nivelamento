
namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        // public Movimento(string idContaCorrente, TipoMovimento tipoMovimento, decimal valor)
        // {
        //     IdMovimento = Guid.NewGuid().ToString();
        //     DataMovimento = DateTime.UtcNow;
        //     IdContaCorrente = idContaCorrente;
        //     TipoMovimento = tipoMovimento;
        //     Valor = valor;
        // }
        public string IdMovimento { get; set; } = null!;
        public string IdContaCorrente { get; set; } = null!;
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }

    }
}
