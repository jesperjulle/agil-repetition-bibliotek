namespace Library.Core;

public class FeeCalculator
{
    public decimal Calculate(DateTime due, DateTime returned)
    {
        if (returned > due)
        {
            return 5m;
        }

        return 0m;
    }
}
