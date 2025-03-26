using Library.Application.Commands;
using Library.Application.Dtos;
using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly LoanService _loanService;

    public LoanController(LoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("borrow")]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBookCommand command)
    {
        try
        {
            await _loanService.BorrowBookAsync(command.BookId, command.UserId);
            return Ok(new { message = "Book borrowed successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command)
    {
        try
        {
            await _loanService.ReturnBookAsync(command.LoanId);
            return Ok(new { message = "Book returned successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
    {
        var result = await _loanService.GetAllLoansAsync();
        return Ok(result);
    }

}
