using Library.Core;
using Moq;

namespace Library.Tests;

[TestClass]
public class LoanServiceTests
{
    private const string Isbn = "9780306406157";

    [TestMethod]
    public void CreateLoan_BogLedigOgIngenLaan_GemmerLaanMed14DagesFrist()
    {
        // Arrange
        var repo = new Mock<ILoanRepository>();
        repo.Setup(r => r.IsBookAvailable(Isbn)).Returns(true);
        repo.Setup(r => r.CountActiveLoans(1)).Returns(0);
        repo.Setup(r => r.Add(It.IsAny<Loan>())).Returns((Loan l) => l);
        var service = new LoanService(repo.Object);
        var today = new DateTime(2026, 11, 16);

        // Act
        var loan = service.CreateLoan(1, Isbn, today);

        // Assert
        Assert.AreEqual(today.AddDays(14), loan.DueDate);
        repo.Verify(r => r.Add(It.IsAny<Loan>()), Times.Once);
    }

    // TODO Del 3: skriv flere tests her
}
