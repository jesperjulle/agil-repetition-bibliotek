namespace Library.Core;

// Gebyr for sen aflevering: 5 kr. pr. dag, dog højst 100 kr.
// Aflevering til tiden eller før er gratis.
public class FeeCalculator
{
    public const decimal FeePerDay = 5m;
    public const decimal MaxFee = 100m;

    public decimal Calculate(DateTime due, DateTime returned)
    {
        int daysLate = (returned.Date - due.Date).Days;

        if (daysLate <= 0)
        {
            return 0m;
        }

        return Math.Min(daysLate * FeePerDay, MaxFee);
    }
}
