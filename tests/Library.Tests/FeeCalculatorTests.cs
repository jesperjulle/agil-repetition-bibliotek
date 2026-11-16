using Library.Core;

namespace Library.Tests;

[TestClass]
public class FeeCalculatorTests
{
    private static readonly DateTime Due = new DateTime(2026, 11, 30);

    // MSTest opretter en ny instans af testklassen for hver test,
    // så hver test får sin egen calculator.
    private readonly FeeCalculator _calculator = new FeeCalculator();

    [TestMethod]
    public void Calculate_AfleveretTilTiden_Returnerer0()
    {
        decimal fee = _calculator.Calculate(Due, Due);

        Assert.AreEqual(0m, fee);
    }

    [TestMethod]
    public void Calculate_AfleveretFoerForfald_Returnerer0()
    {
        var returned = new DateTime(2026, 11, 25);

        decimal fee = _calculator.Calculate(Due, returned);

        Assert.AreEqual(0m, fee);
    }

    [TestMethod]
    public void Calculate_EnDagForSent_Returnerer5()
    {
        var returned = new DateTime(2026, 12, 1);

        decimal fee = _calculator.Calculate(Due, returned);

        Assert.AreEqual(5m, fee);
    }

    [TestMethod]
    public void Calculate_TiDageForSent_Returnerer50()
    {
        var returned = new DateTime(2026, 12, 10);

        decimal fee = _calculator.Calculate(Due, returned);

        Assert.AreEqual(50m, fee);
    }

    [TestMethod]
    public void Calculate_TrediveDageForSent_Returnerer100()
    {
        var returned = new DateTime(2026, 12, 30);

        decimal fee = _calculator.Calculate(Due, returned);

        Assert.AreEqual(100m, fee);
    }
}
