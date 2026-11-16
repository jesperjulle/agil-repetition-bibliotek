namespace Library.Core;

public class FeeCalculator
{
    public const decimal FeePerDay = 5m;

    public decimal Calculate(DateTime due, DateTime returned)
    {
        if (returned > due)
        {
            return FeePerDay;
        }

        return 0m;
    }
}
