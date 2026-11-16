using Library.Core;

namespace Library.Api;

// Data ligger kun i hukommelsen og nulstilles, når API'et genstartes
public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = new();
    private int _nextId = 1;

    public int CountActiveLoans(int memberId) =>
        _loans.Count(l => l.MemberId == memberId && l.ReturnedDate == null);

    public bool IsBookAvailable(string isbn) =>
        !_loans.Any(l => l.BookIsbn == isbn && l.ReturnedDate == null);

    public Loan Add(Loan loan)
    {
        loan.Id = _nextId++;
        _loans.Add(loan);
        return loan;
    }

    public Loan? GetById(int id) => _loans.FirstOrDefault(l => l.Id == id);
}
