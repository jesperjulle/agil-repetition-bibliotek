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
}
