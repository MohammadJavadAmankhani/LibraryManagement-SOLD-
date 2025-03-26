using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);

    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(Guid id) =>
        await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
