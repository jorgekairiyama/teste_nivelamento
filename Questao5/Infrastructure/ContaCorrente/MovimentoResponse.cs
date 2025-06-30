namespace Questao5.Infrastructure.ContaCorrente;

public record MovimentoResponse
{
    public string IdMovimento { get; set; } = null!;
    public string IdContaCorrente { get; set; } = null!;
    //public int NumeroContaCorrente { get; set; }
    public char TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}