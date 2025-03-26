using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Library.Domain.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id);
    Task<IEnumerable<Loan>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Loan>> GetAllWithBookAndUserAsync();
    Task AddAsync(Loan loan);
    Task SaveChangesAsync();
}
