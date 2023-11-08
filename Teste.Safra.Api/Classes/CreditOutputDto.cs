namespace Teste.Safra.Api.Classes;
public class CreditOutputDto
{
    public CreditOutputDto()
    {
        this.Status = "";
        this.Installments = new List<CreditInstallments>();
    }
    public string Status { get; set; }
    public double Value { get; set; }
    public DateTime FirstDueDate { get; set; }
    public double Fees { get; set; }

    public double TotalValue { get; set; }

    public List<CreditInstallments> Installments { get; set; }

}