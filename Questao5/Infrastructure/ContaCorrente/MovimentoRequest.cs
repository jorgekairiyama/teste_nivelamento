namespace Questao5.Infrastructure.ContaCorrente;

    public record MovimentoRequest
    {
        public int NumeroContaCorrente { get; set; }
        public char TipoMovimento { get; set; } 
        public decimal Valor { get; set; }
    }