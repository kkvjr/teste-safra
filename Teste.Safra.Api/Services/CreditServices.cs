using Microsoft.Extensions.Options;
using Teste.Safra.Api.Classes;
using Teste.Safra.Api.Utils;
namespace Teste.Safra.Api.Services;

public class CreditServices
{
    public CreditOutputDto CalcLoan(CreditInputDto input)
    {
        var output = new CreditOutputDto();

        if (input.Value > 1000000.00)
        {
            output.Status = "Negado. Valor solicitado excede o máximo permitido de R$1.000.000,00.";
            return output;
        }

        if (input.Installments < 5 || input.Installments > 72)
        {
            output.Status = "Negado. Número de parcelas fora do limite permitido (mínimo de 5 e máximo de 72).";
            return output;
        }

        if (input.Type == CreditType.CREDITO_PJ && input.Value < 15000.00)
        {
            output.Status = "Negado. Valor mínimo para a modalidade de crédito soliticada é de R$15.000,00";
            return output;
        }

        if (input.FirstDueDate.Date < DateTime.Now.Date.AddDays(15) || input.FirstDueDate.Date > DateTime.Now.Date.AddDays(40))
        {
            output.Status = "Negado. Vencimento da primeira parcela fora limite permitido (mínimo de 15 dias e máximo de 40 dias).";
            return output;
        }


        var fee = CreditType.GetFee(input.Type);

        var totalValue = 0.0;

        var installmentValues = input.Value / input.Installments;

        for (int i = 0; i < input.Installments; i++)
        {
            var installment = new CreditInstallments();
            installment.DueDate = input.FirstDueDate.AddMonths(i).Date;
            installment.Fees = (installmentValues * fee) / 100;
            installment.Value = installmentValues;
            installment.TotalValue = installment.Value + installment.Fees;
            totalValue += installment.TotalValue;
            output.Installments.Add(installment);
        }


        output.Fees = fee / 100;
        output.FirstDueDate = input.FirstDueDate;
        output.Status = "Aprovado";
        output.Value = input.Value;
        output.TotalValue = totalValue;


        return output;

    }



}