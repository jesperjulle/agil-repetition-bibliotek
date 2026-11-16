# Bibliotek-API – Modul 8 repetition

Startkode til repetitionsopgaven.

## Struktur

- `src/Library.Core` – domæne: `Loan`, `ILoanRepository`, `IsbnValidator`, `LoanService`
- `src/Library.Api` – Web API (`POST /api/loans`, `GET /api/loans/{id}`)
- `tests/Library.Tests` – unit tests (MSTest + Moq), ét eksempel i hver fil
- `postman/` – environments til Postman (Local og CI)

## Kom i gang

```bash
dotnet build
dotnet test tests/Library.Tests
dotnet run --project src/Library.Api     # kører på http://localhost:5000
```

Gyldige ISBN-numre: `9780306406157`, `9781861972712`, `9780140449136`

## Refleksion

(Skriv jeres svar her)
