using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Application.Dtos;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;


namespace Library.Application.Services;

public class LoanService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public LoanService(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _loanRepository = loanRepository;
    }

    public async Task BorrowBookAsync(Guid bookId, Guid userId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId)
            ?? throw new Exception("Book not found");

        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found");

        book.Borrow(); 

        var loan = new Loan(bookId, userId);
        await _loanRepository.AddAsync(loan);
        await _bookRepository.SaveChangesAsync();
        await _loanRepository.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(Guid loanId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId)
            ?? throw new Exception("Loan not found");

        if (loan.IsReturned)
            throw new Exception("Book already returned");

        var book = await _bookRepository.GetByIdAsync(loan.BookId)
            ?? throw new Exception("Book not found");

        loan.ReturnBook(); 
        book.Return();      

        await _loanRepository.SaveChangesAsync();
        await _bookRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllWithBookAndUserAsync();

        return loans.Select(l => new LoanDto
        {
            LoanId = l.Id,
            BookTitle = l.Book?.Title ?? "Unknown",
            UserFullName = l.User?.FullName ?? "Unknown",
            BorrowedAt = l.BorrowedAt,
            ReturnedAt = l.ReturnedAt
        });
    }

}
