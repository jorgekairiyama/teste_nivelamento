namespace Questao5.Infrastructure.ContaCorrente;

public record SaldoResponse
{
    public string IdContaCorrente { get; set; } = null!;
    public int Numero { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Valor { get; set; }

}
