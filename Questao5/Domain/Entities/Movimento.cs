
namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; set; } = null!;
        public string IdContaCorrente { get; set; } = null!;
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }

    }
}
