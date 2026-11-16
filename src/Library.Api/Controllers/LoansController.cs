using Library.Core;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly LoanService _service;
    private readonly ILoanRepository _repo;

    public LoansController(LoanService service, ILoanRepository repo)
    {
        _service = service;
        _repo = repo;
    }

    // POST api/loans
    [HttpPost]
    public IActionResult Create(CreateLoanRequest request)
    {
        if (!IsbnValidator.IsValid(request.Isbn))
            return BadRequest(new { error = "Ugyldigt ISBN" });

        try
        {
            var loan = _service.CreateLoan(request.MemberId, request.Isbn, DateTime.Today);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    // GET api/loans/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var loan = _repo.GetById(id);
        return loan is null ? NotFound() : Ok(loan);
    }
}

public record CreateLoanRequest(int MemberId, string Isbn);
