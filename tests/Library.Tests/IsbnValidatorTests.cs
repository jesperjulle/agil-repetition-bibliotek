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

    // TODO Del 2: skriv flere tests her
}
