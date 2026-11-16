namespace Library.Core;

public class LoanService
{
    public const int MaxActiveLoans = 3;
    private readonly ILoanRepository _repo;

    public LoanService(ILoanRepository repo) => _repo = repo;

    public Loan CreateLoan(int memberId, string isbn, DateTime today)
    {
        if (!_repo.IsBookAvailable(isbn))
            throw new InvalidOperationException("Bogen er allerede udlånt");

        if (_repo.CountActiveLoans(memberId) >= MaxActiveLoans)
            throw new InvalidOperationException("Medlemmet har allerede 3 aktive lån");

        var loan = new Loan
        {
            MemberId = memberId,
            BookIsbn = isbn,
            LoanDate = today,
            DueDate = today.AddDays(14)
        };
        return _repo.Add(loan);
    }
}
