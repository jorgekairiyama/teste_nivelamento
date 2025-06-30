namespace Questao5.Infrastructure.ContaCorrente;

    public record ContaCorrenteResponse
    {
    public string IdContaCorrente { get; set; } = string.Empty;
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    
    }