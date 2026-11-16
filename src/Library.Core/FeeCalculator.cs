namespace Library.Core;

public class FeeCalculator
{
    public const decimal FeePerDay = 5m;

    public decimal Calculate(DateTime due, DateTime returned)
    {
        int daysLate = (returned - due).Days;

        if (daysLate > 0)
        {
            return daysLate * FeePerDay;
        }

        return 0m;
    }
}
