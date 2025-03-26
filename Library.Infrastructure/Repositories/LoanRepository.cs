using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Library.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Loan loan) => await _context.Loans.AddAsync(loan);

    public async Task<IEnumerable<Loan>> GetByUserIdAsync(Guid userId) =>
        await _context.Loans.Where(l => l.UserId == userId).ToListAsync();

    public async Task<Loan?> GetByIdAsync(Guid id) =>
        await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Loan>> GetAllWithBookAndUserAsync()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .ToListAsync();
    }

}
