using Library.Api;
using Library.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ILoanRepository, InMemoryLoanRepository>();
builder.Services.AddScoped<LoanService>();

var app = builder.Build();

app.MapControllers();

app.Run();

// Gør Program synlig for evt. integrationstests
public partial class Program { }
