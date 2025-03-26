using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Library.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task SaveChangesAsync();
}
