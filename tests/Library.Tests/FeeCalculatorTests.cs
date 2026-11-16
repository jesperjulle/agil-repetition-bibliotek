using Library.Core;

namespace Library.Tests;

[TestClass]
public class FeeCalculatorTests
{
    [TestMethod]
    public void Calculate_AfleveretTilTiden_Returnerer0()
    {
        var calculator = new FeeCalculator();
        var due = new DateTime(2026, 11, 30);

        decimal fee = calculator.Calculate(due, due);

        Assert.AreEqual(0m, fee);
    }

    [TestMethod]
    public void Calculate_AfleveretFoerForfald_Returnerer0()
    {
        var calculator = new FeeCalculator();
        var due = new DateTime(2026, 11, 30);
        var returned = new DateTime(2026, 11, 25);

        decimal fee = calculator.Calculate(due, returned);

        Assert.AreEqual(0m, fee);
    }

    [TestMethod]
    public void Calculate_EnDagForSent_Returnerer5()
    {
        var calculator = new FeeCalculator();
        var due = new DateTime(2026, 11, 30);
        var returned = new DateTime(2026, 12, 1);

        decimal fee = calculator.Calculate(due, returned);

        Assert.AreEqual(5m, fee);
    }
}
