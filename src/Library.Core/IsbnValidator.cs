namespace Library.Core;

public static class IsbnValidator
{
    // ISBN-13: cifrene vægtes skiftevis 1 og 3, summen skal gå op i 10
    public static bool IsValid(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return false;

        var digits = isbn.Replace("-", "");
        if (digits.Length != 13 || !digits.All(char.IsDigit)) return false;

        int sum = 0;
        for (int i = 0; i < 13; i++)
            sum += (digits[i] - '0') * (i % 2 == 0 ? 1 : 3);

        return sum % 10 == 0;
    }
}
