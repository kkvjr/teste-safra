using Teste.Safra.Api.Services;
using Teste.Safra.Api.Classes;
namespace TesteSafraApi.Tests;

public class CreditServiceTest
{

    [Fact]
    public void IsApproved()
    {
        var input = new CreditInputDto();

        input.FirstDueDate = DateTime.Now.AddDays(20);
        input.Installments = 7;
        input.Type = "Crédito Consignado";
        input.Value = 10000.00;
        var creditServices = new CreditServices();

        var output = creditServices.CalcLoan(input);

        Assert.True(output.Status.Contains("Aprovado"), "Concessão de crédito aprovada");
    }



    [Fact]
    public void NotApproved()
    {
        var input = new CreditInputDto();

        input.FirstDueDate = DateTime.Now.AddDays(20);
        input.Installments = 7;
        input.Type = "Crédito Pessoa Jurídica";
        input.Value = 10000.00;
        var creditServices = new CreditServices();

        var output = creditServices.CalcLoan(input);

        Assert.True(output.Status.Contains("Negado"), "Concessão de crédito pessoa jurídica negada");
    }



    [Fact]
    public void PJApproved()
    {
        var input = new CreditInputDto();

        input.FirstDueDate = DateTime.Now.AddDays(20);
        input.Installments = 7;
        input.Type = "Crédito Pessoa Jurídica";
        input.Value = 15000.00;
        var creditServices = new CreditServices();

        var output = creditServices.CalcLoan(input);

        Assert.True(output.Status.Contains("Aprovado"), "Concessão de crédito pessoa jurídica aprovada");
    }

    [Fact]
    public void MaxValueApproved()
    {
        var input = new CreditInputDto();

        input.FirstDueDate = DateTime.Now.AddDays(20);
        input.Installments = 7;
        input.Type = "Crédito Pessoa Jurídica";
        input.Value = 1000000.00;
        var creditServices = new CreditServices();

        var output = creditServices.CalcLoan(input);

        Assert.True(output.Status.Contains("Aprovado"), "Concessão de crédito aprovada");
    }
}