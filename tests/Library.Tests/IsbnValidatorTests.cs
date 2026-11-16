using Library.Core;

namespace Library.Tests;

[TestClass]
public class IsbnValidatorTests
{
    [TestMethod]
    public void IsValid_GyldigtIsbn13_ReturnererTrue()
    {
        // Arrange
        var isbn = "9780306406157";

        // Act
        var result = IsbnValidator.IsValid(isbn);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_GyldigtIsbnMedBindestreger_ReturnererTrue()
    {
        // Arrange
        var isbn = "978-0-306-40615-7";

        // Act
        var result = IsbnValidator.IsValid(isbn);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_ForkertKontrolciffer_ReturnererFalse()
    {
        // Arrange
        var isbn = "9780306406158";

        // Act
        var result = IsbnValidator.IsValid(isbn);

        // Assert
        Assert.IsFalse(result);
    }

    // TODO Del 2: skriv flere tests her
}
