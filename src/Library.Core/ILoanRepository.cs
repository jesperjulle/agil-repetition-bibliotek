namespace Library.Core;

public interface ILoanRepository
{
    int CountActiveLoans(int memberId);
    bool IsBookAvailable(string isbn);
    Loan Add(Loan loan);
    Loan? GetById(int id);
}
