namespace Questao5.Domain.Entities
{
    public class Saldo
    {
        public string IdContaCorrente { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}