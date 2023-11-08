namespace Teste.Safra.Api.Utils;
public static class CreditType
{
    public static readonly string CREDITO_DIRETO = "Crédito Direto";
    public static readonly string CREDITO_CONSIGNADO = "Crédito Consignado";
    public static readonly string CREDITO_PJ = "Crédito Pessoa Jurídica";
    public static readonly string CREDITO_PF = "Crédito Pessoa Física";
    public static readonly string CREDITO_IMOBILIARIO = "Crédito Imobiliário";

    public static double GetFee(string Type)
    {
        var fee = 9.0;
        if (Type == CreditType.CREDITO_DIRETO)
        {
            fee = 2.00;
        }

        if (Type == CreditType.CREDITO_CONSIGNADO)
        {
            fee = 1.00;
        }

        if (Type == CreditType.CREDITO_PJ)
        {
            fee = 5.00;
        }

        if (Type == CreditType.CREDITO_PF)
        {
            fee = 3.00;
        }

        return fee;


    }

}