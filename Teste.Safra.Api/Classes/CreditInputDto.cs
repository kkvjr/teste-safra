namespace Teste.Safra.Api.Classes;
public class CreditInputDto
{
    public double Value { get; set; }

    public string Type { get; set; }

    public int Installments { get; set; }
    public DateTime FirstDueDate { get; set; }

}